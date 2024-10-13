using System.Security.Cryptography;

namespace Advent;

public static class Encryption
{
    // Code adapted from https://learn.microsoft.com/en-us/dotnet/standard/security/walkthrough-creating-a-cryptographic-application

    private static bool TryReadKeyFromStore(string keyName, out RSACryptoServiceProvider rsaProvider)
    {
        rsaProvider = new(2048);
        CspParameters cspParams = new() { KeyContainerName = keyName, Flags = CspProviderFlags.UseExistingKey | CspProviderFlags.UseMachineKeyStore }; try
        {
            rsaProvider = new(2048, cspParams);
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error retrieving key from store - {ex.Message}");
            return false;
        }
    }

    public static bool Encrypt(string inputFile, string outputFile, string keyName)
    {
        if (!TryReadKeyFromStore(keyName, out RSACryptoServiceProvider rsaProvider)) return false;
        FileInfo file = new(inputFile);
        string stage = "Encrypt Key";
        try
        {
            // Create instance of Aes for symmetric encryption of the data.
            Aes aes = Aes.Create();
            ICryptoTransform transform = aes.CreateEncryptor();

            // Use RSACryptoServiceProvider to encrypt the AES key.
            byte[] keyEncrypted = rsaProvider.Encrypt(aes.Key, false);

            stage = "Prepare Arrays";
            // Create byte arrays to contain the length values of the key and IV.
            int lKey = keyEncrypted.Length;
            byte[] LenK = BitConverter.GetBytes(lKey);
            int lIV = aes.IV.Length;
            byte[] LenIV = BitConverter.GetBytes(lIV);

            // Write the following to the FileStream for the encrypted file (outFs):
            // - length of the key, length of the IV, encrypted key, the IV, the encrypted cipher content
            using var outFs = new FileStream(outputFile, FileMode.Create);
            outFs.Write(LenK, 0, 4);
            outFs.Write(LenIV, 0, 4);
            outFs.Write(keyEncrypted, 0, lKey);
            outFs.Write(aes.IV, 0, lIV);

            stage = "Do Encryption";
            // Now write the cipher text using a CryptoStream for encrypting.
            using CryptoStream outStreamEncrypted = new(outFs, transform, CryptoStreamMode.Write);
            // By encrypting a chunk at a time, you can save memory and accommodate large files.
            int count = 0;
            int offset = 0;

            // blockSizeBytes can be any arbitrary size.
            int blockSizeBytes = aes.BlockSize / 8;
            byte[] data = new byte[blockSizeBytes];
            int bytesRead = 0;

            using (FileStream inFs = new(file.FullName, FileMode.Open))
            {
                do
                {
                    count = inFs.Read(data, 0, blockSizeBytes);
                    offset += count;
                    outStreamEncrypted.Write(data, 0, count);
                    bytesRead += blockSizeBytes;
                } while (count > 0);
            }
            outStreamEncrypted.FlushFinalBlock();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error in encryption at stage: {stage} - {ex.Message}");
            return false;
        }
    }

    public static bool Decrypt(string inputFile, string outputFile, string keyName)
    {
        if (!TryReadKeyFromStore(keyName, out RSACryptoServiceProvider rsaProvider)) return false;
        FileInfo file = new(inputFile);
        string stage = "Read File";
        try
        {
            // Create instance of Aes for symmetric decryption of the data.
            Aes aes = Aes.Create();

            // Create byte arrays to get the length of the encrypted key and IV.  These values were stored as 4 bytes each at the beginning of the encrypted package.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            // Use FileStream objects to read the encrypted file (inFs) and save the decrypted file (outFs).
            using FileStream inFs = new(file.FullName, FileMode.Open);
            inFs.Seek(0, SeekOrigin.Begin);
            inFs.Read(LenK, 0, 3);
            inFs.Seek(4, SeekOrigin.Begin);
            inFs.Read(LenIV, 0, 3);

            // Convert the lengths to integer values.
            int lenK = BitConverter.ToInt32(LenK, 0);
            int lenIV = BitConverter.ToInt32(LenIV, 0);

            // Determine the start position of the cipher text (startC) and its length(lenC).
            int startC = lenK + lenIV + 8;
            int lenC = (int)inFs.Length - startC;

            stage = "Extract keys";
            // Create the byte arrays for the encrypted Aes key, the IV, and the cipher text.
            byte[] KeyEncrypted = new byte[lenK];
            byte[] IV = new byte[lenIV];

            // Extract the key and IV starting from index 8 after the length values.
            inFs.Seek(8, SeekOrigin.Begin);
            inFs.Read(KeyEncrypted, 0, lenK);
            inFs.Seek(8 + lenK, SeekOrigin.Begin);
            inFs.Read(IV, 0, lenIV);

            // Use RSACryptoServiceProvider to decrypt the AES key.
            stage = "Decrypt Key";
            byte[] KeyDecrypted = rsaProvider.Decrypt(KeyEncrypted, false);

            // Decrypt the key.
            stage = "Create Decryptor";
            ICryptoTransform transform = aes.CreateDecryptor(KeyDecrypted, IV);

            // Decrypt the cipher text from from the FileSteam of the encrypted file (inFs) into the FileStream for the decrypted file (outFs).
            stage = "Create Filestream";
            using FileStream outFs = new(outputFile, FileMode.Create);
            int count = 0;
            int offset = 0;

            // By decrypting a chunk a time, you can save memory and accommodate large files.
            // blockSizeBytes can be any arbitrary size.
            int blockSizeBytes = aes.BlockSize / 8;
            byte[] data = new byte[blockSizeBytes];

            // Start at the beginning of the cipher text.
            inFs.Seek(startC, SeekOrigin.Begin);
            stage = "Decrypt File";
            using CryptoStream outStreamDecrypted = new(outFs, transform, CryptoStreamMode.Write);
            do
            {
                count = inFs.Read(data, 0, blockSizeBytes);
                offset += count;
                outStreamDecrypted.Write(data, 0, count);
            } while (count > 0);

            outStreamDecrypted.FlushFinalBlock();
            return true;

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error in decryption at stage: {stage} - {ex.Message}");
            return false;
        }
    }
}
