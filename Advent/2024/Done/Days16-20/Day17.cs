namespace Advent2024;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        long [] registers = [InputNumbers[0][0], InputNumbers[1][0], InputNumbers[2][0]], program = InputNumbers[3];
        if(Part1 && TestMode) RunTests();
        Output = (Part1, TestMode) switch
        {
            (true, _) => RunCode(program, registers),
            (_, true) => RunCode2Naive(program),
            (_, _) => RunCode2(program),
        };
    }

    private static string RunCode(long[] program, long[] registers)
    {
        int pointer = 0;
        List<long> outputs = [];
        do
        {
            if (TryGetOutputFromOperation((int)program[pointer], (int)program[pointer + 1], registers, ref pointer, out long output))
                outputs.Add(output);
        } while (pointer < program.Length);
        return string.Join(',', outputs);
    }

    /*
    The magic numbers in RunCode2 are derived by noticing patterns in the binary of the numbers which produce each even output in turn

    Output			      Number ends with this			                        or this
    2                                     00010                                   11011
    4                         00001_10000_00010                       10101_00111_11011
    6                   01110_00001_10000_00010                 01110_10101_00111_11011
    8             01110_01110_00001_10000_00010           01110_01110_10101_00111_11011
    10       1111_01110_01110_00001_10000_00010      1111_01110_01110_10101_00111_11011
    12  0111_1111_01110_01110_00001_10000_00010 0111_1111_01110_01110_10101_00111_11011

    output	v1		    v2		    step
    2	    2		    27		    32
    4	    1538		21755		1024
    6	    460290		480507		1048576
    8	    15140354	15160571	33554432
    10	    518456834	518477051   536870912
    12	    4276553218	4276573435	8589934592
    */

    private static string RunCode2(long[] program)
    {
        long result = long.MaxValue;
        long[] tests = [4276553218, 4276573435];
        long step = 8589934592;
        for (int i = 0; i < 2; i++)
        {
            bool foundMatch = false;
            long test = tests[i];
            do
            {
                int currentOutput = 0, pointer = 0;
                long[] registers = [test, 0, 0];
                do
                {
                    if (TryGetOutputFromOperation((int)program[pointer], (int)program[pointer + 1], registers, ref pointer, out long output))
                    {
                        if (output != program[currentOutput]) break;
                        else if (++currentOutput == program.Length) foundMatch = true;
                    }
                } while (pointer < program.Length && !foundMatch);
                if (!foundMatch) test += step;
            } while (!foundMatch);
            result = Math.Min(result, test);
        }
        return result.ToString();
    }

    private static string RunCode2Naive(long[] program)
    {
        Debug.Print(string.Join(",", program));
        bool foundMatch = false;
        long test = 0;
        do
        {
            int currentOutput = 0, pointer = 0;
            long[] registers = [test, 0, 0];
            do
            {
                if (TryGetOutputFromOperation((int)program[pointer], (int)program[pointer + 1], registers, ref pointer, out long output))
                {
                    if (output != program[currentOutput]) break;
                    else if (++currentOutput == program.Length) foundMatch = true;
                }
            } while (pointer < program.Length);
            if (!foundMatch) test ++;
        } while (!foundMatch);
        return test.ToString();
    }

    private static bool TryGetOutputFromOperation(int opCode, int opVal, long[] registers, ref int pointer, out long output)
    {
        long comboVal = opVal == 7 ? 0 : opVal < 4 ? opVal : registers[opVal % 4];
        output = -1;
        switch (opCode)
        {
            case 0:
                //The adv instruction (opcode 0) performs division
                //The numerator is the value in the A register.
                //The denominator is found by raising 2 to the power of the instruction's combo operand.#
                //(So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.)
                //The result of the division operation is truncated to an integer and then written to the A register.
                registers[0] = registers[0] / (int)Math.Pow(2, comboVal);
                break;
            case 1:
                //The bxl instruction(opcode 1) calculates the bitwise XOR
                //of register B and the instruction's literal operand, then stores the result in register B.
                registers[1] = registers[1] ^ opVal;
                break;
            case 2:
                //The bst instruction (opcode 2) calculates the value of its combo operand modulo 8
                //(thereby keeping only its lowest 3 bits), then writes that value to the B register.
                registers[1] = comboVal % 8;
                break;
            case 3:
                //The jnz instruction (opcode 3) does nothing if the A register is 0.
                // if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand
                // if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
                if (registers[0] != 0)
                    pointer = opVal;
                else
                    pointer += 2;
                break;
            case 4:
                //The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C,
                //then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)
                registers[1] = registers[1] ^ registers[2];
                break;
            case 5:
                //The out instruction (opcode 5) calculates the value of its combo operand modulo 8,
                //then outputs that value. (If a program outputs multiple values, they are separated by commas.)
                output = comboVal % 8;
                pointer += 2;
                return true;
            case 6:
                //The bdv instruction (opcode 6) works exactly like the adv instruction
                //except that the result is stored in the B register. (The numerator is still read from the A register.)
                registers[1] = registers[0] / (int)Math.Pow(2, comboVal);
                break;
            case 7:
                //The cdv instruction (opcode 7) works exactly like the adv instruction
                //except that the result is stored in the C register. (The numerator is still read from the A register.)
                registers[2] = registers[0] / (int)Math.Pow(2, comboVal);
                break;
            default:
                break;
        }
        if (opCode != 3) pointer += 2;
        return false;
    }

    private static void RunTests()
    {
        long[] registers;
        long[] program;
        
        //If register C contains 9, the program 2,6 would set register B to 1.
        registers = [0,0,9];
        program = [2, 6];
        RunCode(program, registers);
        Debug.Assert(registers[1] == 1);

        //If register A contains 10, the program 5,0,5,1,5,4 would output 0,1,2.
        registers = [10, 0, 0];
        program = [5, 0, 5, 1, 5, 4];
        Debug.Assert(RunCode(program, registers) == "0,1,2");

        //If register A contains 2024, the program 0,1,5,4,3,0 would output 4,2,5,6,7,7,7,7,3,1,0 and leave 0 in register A.
        registers = [2024, 0, 0];
        program = [0, 1, 5, 4, 3, 0];
        Debug.Assert(RunCode(program, registers) == "4,2,5,6,7,7,7,7,3,1,0" && registers[0] == 0);

        //If register B contains 29, the program 1,7 would set register B to 26.
        registers = [0, 29, 0];
        program = [1, 7];
        RunCode(program, registers);
        Debug.Assert(registers[1] == 26);

        //If register B contains 2024 and register C contains 43690, the program 4,0 would set register B to 44354.
        registers = [0, 2024, 43690];
        program = [4, 0];
        RunCode(program, registers);
        Debug.Assert(registers[1] == 44354);
    }
}
