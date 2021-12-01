﻿namespace Advent2019;

public partial class Day14 : Advent.Day
{
    /*
        *  Description -   Input is a series of reactions needed to make fuel from ore.
        *  
        *  Part 1 -        What is the minimum amount of ore needed to make 1 unit of fuel?
        *  Part 2 -        You have a trillion units of ore available.  What is the maximum amount of fuel you can make?
    */
    public Day14(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("10 ORE => 10 A;1 ORE => 1 B;7 A, 1 B => 1 C;7 A, 1 C => 1 D;7 A, 1 D => 1 E;7 A, 1 E => 1 FUEL");
                Expecteds.Add("31");
                AddInput("9 ORE => 2 A;8 ORE => 3 B;7 ORE => 5 C;3 A, 4 B => 1 AB;5 B, 7 C => 1 BC;4 C, 1 A => 1 CA;2 AB, 3 BC, 4 CA => 1 FUEL");
                Expecteds.Add("165");
                AddInput("157 ORE => 5 NZVS;165 ORE => 6 DCFZ;44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL;12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ;179 ORE => 7 PSHF;177 ORE => 5 HKGWZ;7 DCFZ, 7 PSHF => 2 XJWVT;165 ORE => 2 GPVTF;3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT");
                Expecteds.Add("13312");
                AddInput("2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG;17 NVRVD, 3 JNWZP => 8 VPVL;53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL;22 VJHF, 37 MNCFX => 5 FWMGM;139 ORE => 4 NVRVD;144 ORE => 7 JNWZP;5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC;5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV;145 ORE => 6 MNCFX;1 NVRVD => 8 CXFTF;1 VJHF, 6 MNCFX => 4 RFSQX;176 ORE => 6 VJHF");
                Expecteds.Add("180697");
                AddInput("171 ORE => 8 CNZTR;7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL;114 ORE => 4 BHXH;14 VRPVC => 6 BMBT;6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL;6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT;15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW;13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW;5 BMBT => 4 WPTQ;189 ORE => 9 KTJDG;1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP;12 VRPVC, 27 CNZTR => 2 XDBXC;15 KTJDG, 12 BHXH => 5 XCVML;3 BHXH, 2 VRPVC => 7 MZWV;121 ORE => 7 VRPVC;7 XCVML => 6 RJRHP;5 BHXH, 4 VRPVC => 5 LTCX");
                Expecteds.Add("2210736");
                break;
            case (1, false):
                AddInput("1 XVCBM, 12 SWPQ => 7 VMWSR;10 SBLTQ, 14 TLDR => 6 HJFPQ;1 VWHXC, 2 GZDQ, 3 PCLMJ => 4 VJPLN;9 MGVG => 7 WDPF;1 FBXD, 5 FZNZR => 6 GZDQ;5 TJPZ, 1 QNMZ => 5 SWPQ;12 XWQW, 1 HJFPQ => 8 JPKNC;15 CPNC, 2 TXKRN, 2 MTVQD => 9 LBRSX;5 VJPLN, 1 VSTRK, 2 GFQLV => 5 NLZKH;1 TLDR => 4 TNRZW;2 VCFM => 7 FZNZR;1 PSTRV, 5 RTDV => 8 VCFM;2 PSTRV => 9 SFWJG;4 XWQW => 2 BHPS;1 ZWFNW, 19 JKRWT, 2 JKDL, 8 PCLMJ, 7 FHNL, 22 MSZCF, 1 VSTRK, 7 DMJPR => 1 ZDGF;22 XVCBM, 8 TBLM => 1 MTVQD;101 ORE => 1 WBNWZ;6 VNVXJ, 1 FBXD, 13 PCLMJ => 9 MGVG;13 SHWB, 1 WDPF, 4 QDTW => 6 FHNL;9 VSTRK => 2 VZCML;20 LZCDB => 7 KNPM;2 LBRSX, 9 GRCD => 3 SHWB;5 BHPS => 6 SQJLW;1 RTDV => 6 GRCD;6 SBLTQ, 6 XWQW => 5 CPNC;153 ORE => 3 RTDV;6 LZCDB, 1 SBLTQ => 3 PCLMJ;1 RTDV, 2 TJPZ => 5 LZCDB;24 QNMZ => 4 TXKRN;19 PCLMJ, 7 VNVXJ => 6 RKRVJ;12 RKRVJ, 11 QNMZ => 3 JKRWT;4 SFWJG => 9 FBXD;16 WDPF, 4 TXKRN => 6 DMJPR;3 QNMZ => 1 VSTRK;9 VSTRK => 4 ZWFNW;7 QBWN, 1 TLDR => 4 QDTW;7 VJPLN, 1 NLZKH, 15 JPKNC, 3 SHWB, 1 MSZCF, 3 VMWSR => 6 QDHGS;14 QXQZ => 7 XWQW;152 ORE => 9 TJPZ;1 PJVJ, 10 QBWN, 19 NLZKH => 6 MSZCF;21 TLDR, 13 VNVXJ, 5 BHPS => 4 QBWN;1 GZDQ, 6 GRCD => 9 TLDR;4 BHPS => 8 MZBL;1 FZNZR => 2 VNVXJ;1 VNVXJ => 5 GFQLV;13 LZCDB => 2 QXQZ;3 MNFJX => 5 VWHXC;1 GZDQ, 2 VMWSR => 6 WZMHW;9 HJFPQ, 3 RKRVJ => 4 QNMZ;8 TJPZ => 9 SBLTQ;30 WBNWZ => 5 TBLM;1 PCLMJ => 3 GNMTQ;30 SQJLW, 3 QNMZ, 9 WDPF => 5 PJVJ;10 GRCD, 15 SBLTQ, 22 GFQLV => 4 XVCBM;30 PJVJ, 10 JPKNC, 3 DXFDR, 10 VZCML, 59 MZBL, 40 VWHXC, 1 ZDGF, 13 QDHGS => 1 FUEL;4 GNMTQ, 6 VMWSR, 19 RKRVJ, 5 FKZF, 4 VCFM, 2 WZMHW, 7 KNPM, 5 TNRZW => 7 DXFDR;152 ORE => 9 PSTRV;2 BHPS, 5 TXKRN, 2 PJVJ => 4 FKZF;2 XWQW, 2 VCFM, 13 BHPS => 8 MNFJX;3 XWQW => 2 JKDL");
                Expecteds.Add("374457");
                break;
            case (2, true):
                AddInput("157 ORE => 5 NZVS;165 ORE => 6 DCFZ;44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL;12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ;179 ORE => 7 PSHF;177 ORE => 5 HKGWZ;7 DCFZ, 7 PSHF => 2 XJWVT;165 ORE => 2 GPVTF;3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT");
                Expecteds.Add("82892753");
                AddInput("2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG;17 NVRVD, 3 JNWZP => 8 VPVL;53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL;22 VJHF, 37 MNCFX => 5 FWMGM;139 ORE => 4 NVRVD;144 ORE => 7 JNWZP;5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC;5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV;145 ORE => 6 MNCFX;1 NVRVD => 8 CXFTF;1 VJHF, 6 MNCFX => 4 RFSQX;176 ORE => 6 VJHF");
                Expecteds.Add("5586022");
                AddInput("171 ORE => 8 CNZTR;7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL;114 ORE => 4 BHXH;14 VRPVC => 6 BMBT;6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL;6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT;15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW;13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW;5 BMBT => 4 WPTQ;189 ORE => 9 KTJDG;1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP;12 VRPVC, 27 CNZTR => 2 XDBXC;15 KTJDG, 12 BHXH => 5 XCVML;3 BHXH, 2 VRPVC => 7 MZWV;121 ORE => 7 VRPVC;7 XCVML => 6 RJRHP;5 BHXH, 4 VRPVC => 5 LTCX");
                Expecteds.Add("460664");
                break;
            case (2, false):
                AddInput("1 XVCBM, 12 SWPQ => 7 VMWSR;10 SBLTQ, 14 TLDR => 6 HJFPQ;1 VWHXC, 2 GZDQ, 3 PCLMJ => 4 VJPLN;9 MGVG => 7 WDPF;1 FBXD, 5 FZNZR => 6 GZDQ;5 TJPZ, 1 QNMZ => 5 SWPQ;12 XWQW, 1 HJFPQ => 8 JPKNC;15 CPNC, 2 TXKRN, 2 MTVQD => 9 LBRSX;5 VJPLN, 1 VSTRK, 2 GFQLV => 5 NLZKH;1 TLDR => 4 TNRZW;2 VCFM => 7 FZNZR;1 PSTRV, 5 RTDV => 8 VCFM;2 PSTRV => 9 SFWJG;4 XWQW => 2 BHPS;1 ZWFNW, 19 JKRWT, 2 JKDL, 8 PCLMJ, 7 FHNL, 22 MSZCF, 1 VSTRK, 7 DMJPR => 1 ZDGF;22 XVCBM, 8 TBLM => 1 MTVQD;101 ORE => 1 WBNWZ;6 VNVXJ, 1 FBXD, 13 PCLMJ => 9 MGVG;13 SHWB, 1 WDPF, 4 QDTW => 6 FHNL;9 VSTRK => 2 VZCML;20 LZCDB => 7 KNPM;2 LBRSX, 9 GRCD => 3 SHWB;5 BHPS => 6 SQJLW;1 RTDV => 6 GRCD;6 SBLTQ, 6 XWQW => 5 CPNC;153 ORE => 3 RTDV;6 LZCDB, 1 SBLTQ => 3 PCLMJ;1 RTDV, 2 TJPZ => 5 LZCDB;24 QNMZ => 4 TXKRN;19 PCLMJ, 7 VNVXJ => 6 RKRVJ;12 RKRVJ, 11 QNMZ => 3 JKRWT;4 SFWJG => 9 FBXD;16 WDPF, 4 TXKRN => 6 DMJPR;3 QNMZ => 1 VSTRK;9 VSTRK => 4 ZWFNW;7 QBWN, 1 TLDR => 4 QDTW;7 VJPLN, 1 NLZKH, 15 JPKNC, 3 SHWB, 1 MSZCF, 3 VMWSR => 6 QDHGS;14 QXQZ => 7 XWQW;152 ORE => 9 TJPZ;1 PJVJ, 10 QBWN, 19 NLZKH => 6 MSZCF;21 TLDR, 13 VNVXJ, 5 BHPS => 4 QBWN;1 GZDQ, 6 GRCD => 9 TLDR;4 BHPS => 8 MZBL;1 FZNZR => 2 VNVXJ;1 VNVXJ => 5 GFQLV;13 LZCDB => 2 QXQZ;3 MNFJX => 5 VWHXC;1 GZDQ, 2 VMWSR => 6 WZMHW;9 HJFPQ, 3 RKRVJ => 4 QNMZ;8 TJPZ => 9 SBLTQ;30 WBNWZ => 5 TBLM;1 PCLMJ => 3 GNMTQ;30 SQJLW, 3 QNMZ, 9 WDPF => 5 PJVJ;10 GRCD, 15 SBLTQ, 22 GFQLV => 4 XVCBM;30 PJVJ, 10 JPKNC, 3 DXFDR, 10 VZCML, 59 MZBL, 40 VWHXC, 1 ZDGF, 13 QDHGS => 1 FUEL;4 GNMTQ, 6 VMWSR, 19 RKRVJ, 5 FKZF, 4 VCFM, 2 WZMHW, 7 KNPM, 5 TNRZW => 7 DXFDR;152 ORE => 9 PSTRV;2 BHPS, 5 TXKRN, 2 PJVJ => 4 FKZF;2 XWQW, 2 VCFM, 13 BHPS => 8 MNFJX;3 XWQW => 2 JKDL");
                Expecteds.Add("3568888");
                break;
        }
    }
}
