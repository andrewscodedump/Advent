namespace Advent2015;

public partial class Day19 : Advent.Day
{
    /*
        *  Description -   This is a molecular manipulation machine.  The last part of the input gives the target molecule and the remainder give all of the available manipulations.
        *  
        *  Part 1 -        How many distinct molecules can be built from the starting molecule by applying one only of the manipulations?
        *  Part 2 -        Starting from a single "e", what is the minimum number of manipulations that will give the target molecule?
    */

    public Day19(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        BatchStatus = DayBatchStatus.Performance;
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("H => HO;H => OH;O => HH;HOHOHO");
                Expecteds.Add("7");
                break;
            case (1, false):
                Inputs.Add("Al => ThF;Al => ThRnFAr;B => BCa;B => TiB;B => TiRnFAr;Ca => CaCa;Ca => PB;Ca => PRnFAr;Ca => SiRnFYFAr;Ca => SiRnMgAr;Ca => SiTh;F => CaF;F => PMg;F => SiAl;H => CRnAlAr;H => CRnFYFYFAr;H => CRnFYMgAr;H => CRnMgYFAr;H => HCa;H => NRnFYFAr;H => NRnMgAr;H => NTh;H => OB;H => ORnFAr;Mg => BF;Mg => TiMg;N => CRnFAr;N => HSi;O => CRnFYFAr;O => CRnMgAr;O => HP;O => NRnFAr;O => OTi;P => CaP;P => PTi;P => SiRnFAr;Si => CaSi;Th => ThCa;Ti => BP;Ti => TiTi;e => HF;e => NAl;e => OMg;ORnPBPMgArCaCaCaSiThCaCaSiThCaCaPBSiRnFArRnFArCaCaSiThCaCaSiThCaCaCaCaCaCaSiRnFYFArSiRnMgArCaSiRnPTiTiBFYPBFArSiRnCaSiRnTiRnFArSiAlArPTiBPTiRnCaSiAlArCaPTiTiBPMgYFArPTiRnFArSiRnCaCaFArRnCaFArCaSiRnSiRnMgArFYCaSiRnMgArCaCaSiThPRnFArPBCaSiRnMgArCaCaSiThCaSiRnTiMgArFArSiThSiThCaCaSiRnMgArCaCaSiRnFArTiBPTiRnCaSiAlArCaPTiRnFArPBPBCaCaSiThCaPBSiThPRnFArSiThCaSiThCaSiThCaPTiBSiRnFYFArCaCaPRnFArPBCaCaPBSiRnTiRnFArCaPRnFArSiRnCaCaCaSiThCaRnCaFArYCaSiRnFArBCaCaCaSiThFArPBFArCaSiRnFArRnCaCaCaFArSiRnFArTiRnPMgArF");
                Expecteds.Add("576");
                break;
            case (2, true):
                Inputs.Add("H => HO;H => OH;O => HH;HOHOHO");
                Expecteds.Add("7");
                break;
            case (2, false):
                Inputs.Add("Al => ThF;Al => ThRnFAr;B => BCa;B => TiB;B => TiRnFAr;Ca => CaCa;Ca => PB;Ca => PRnFAr;Ca => SiRnFYFAr;Ca => SiRnMgAr;Ca => SiTh;F => CaF;F => PMg;F => SiAl;H => CRnAlAr;H => CRnFYFYFAr;H => CRnFYMgAr;H => CRnMgYFAr;H => HCa;H => NRnFYFAr;H => NRnMgAr;H => NTh;H => OB;H => ORnFAr;Mg => BF;Mg => TiMg;N => CRnFAr;N => HSi;O => CRnFYFAr;O => CRnMgAr;O => HP;O => NRnFAr;O => OTi;P => CaP;P => PTi;P => SiRnFAr;Si => CaSi;Th => ThCa;Ti => BP;Ti => TiTi;e => HF;e => NAl;e => OMg;ORnPBPMgArCaCaCaSiThCaCaSiThCaCaPBSiRnFArRnFArCaCaSiThCaCaSiThCaCaCaCaCaCaSiRnFYFArSiRnMgArCaSiRnPTiTiBFYPBFArSiRnCaSiRnTiRnFArSiAlArPTiBPTiRnCaSiAlArCaPTiTiBPMgYFArPTiRnFArSiRnCaCaFArRnCaFArCaSiRnSiRnMgArFYCaSiRnMgArCaCaSiThPRnFArPBCaSiRnMgArCaCaSiThCaSiRnTiMgArFArSiThSiThCaCaSiRnMgArCaCaSiRnFArTiBPTiRnCaSiAlArCaPTiRnFArPBPBCaCaSiThCaPBSiThPRnFArSiThCaSiThCaSiThCaPTiBSiRnFYFArCaCaPRnFArPBCaCaPBSiRnTiRnFArCaPRnFArSiRnCaCaCaSiThCaRnCaFArYCaSiRnFArBCaCaCaSiThFArPBFArCaSiRnFArRnCaCaCaFArSiRnFArTiRnPMgArF");
                Expecteds.Add("207");
                break;
        }
    }
}
