﻿namespace Advent2020;

public partial class Day09 : Advent.Day
{
    /*
        *  Description -   Input is a list of encrypted numbers
        *  
        *  Part 1 -        What is the first number in the list that cannot be created as a sum of the previous 25 numbers?
        *  Part 2 -        What is the sum of the maximum and minimum values in the first contiguous range of numbers that add up to the number found in part 1?
    */
    public Day09(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("35;20;15;25;47;40;62;55;65;95;102;117;150;182;127;219;299;277;309;576");
                Expecteds.Add("127");
                break;
            case (1, false):
                Inputs.Add("42;27;23;19;4;26;31;29;11;49;38;9;45;1;40;46;39;28;44;16;13;5;32;17;50;12;20;6;7;10;22;14;18;60;59;8;19;9;11;15;21;23;39;24;25;16;26;13;29;17;27;28;20;35;30;31;22;32;44;33;38;47;34;36;37;41;55;64;40;42;43;46;39;45;61;48;50;52;65;62;53;67;72;69;70;74;73;98;86;76;79;81;108;84;82;91;160;153;93;253;100;102;105;115;175;166;151;149;142;218;147;152;162;155;161;163;165;173;255;184;191;193;293;195;357;252;295;220;257;404;289;291;322;294;314;415;343;316;318;324;328;512;364;517;379;384;511;636;615;574;472;477;509;644;1021;605;585;608;610;800;634;640;1122;642;1255;692;841;893;981;763;1313;1586;1046;949;986;1057;1062;1094;1444;1300;2393;1193;1250;1244;1274;1475;1282;1405;1334;1533;1585;1604;1744;2467;1712;2986;2043;2193;2255;2048;3250;2338;2287;2437;3711;2443;2526;2494;2518;2556;3382;2938;2990;3527;4149;5876;4660;4993;4050;4724;4091;4236;4241;4303;4781;4625;4730;4805;4880;4937;5432;7280;9717;5074;5494;9027;5928;9505;7577;8966;8141;8286;8291;13021;12377;10162;9535;8544;13907;14235;14459;9610;9685;19222;17294;10506;18637;18683;14040;33262;22526;21882;15718;17751;16427;16432;16835;35110;28293;18079;19050;28322;34586;30339;40277;19295;26520;20191;24546;26224;26933;29758;33090;47837;34506;34914;32553;64269;34178;52868;62471;35885;37374;37129;38270;38345;96649;39486;43841;57636;70898;44737;46415;68387;66887;74155;71307;85211;66731;67059;67467;68438;110684;82300;73014;80622;74230;76615;75399;106217;77831;152678;126074;116044;91152;111624;113474;141117;133618;133790;138366;134198;173276;142668;157699;135905;158453;206696;212520;147244;157237;365198;152014;239548;168983;191305;224942;202776;254591;276458;272564;247092;267408;267816;402014;301121;292651;278573;506956;283149;293142;304481;309251;494139;565215;320997;343319;441547;371759;785529;449868;692756;457367;537740;592400;514500;761848;546389;711265;561722;1157615;585793;571715;846991;1100293;597623;874466;715078;778364;881059;784866;793187;813306;821627;907235;1452774;971867;995107;1157508;1060889;1076222;1118104;1108111;1340086;1370659;1169338;1183416;1375987;1634933;1508265;1419250;1493442;1499944;2407179;1665925;1578053;1889528;1720541;1728862;1879102;1966974;2400975;3213983;2230227;2137111;3457155;2448197;3379046;2352754;2539997;3306915;2993386;5013979;2912692;2919194;3085175;3071495;3165869;3243978;3545027;3298594;5846912;3449403;3607964;3846076;4104085;4940972;4367338;4489865;6764070;4800951;4892751;5265446;5271948;6362095;5831886;6291980;6237364;5984187;6615272;6156670;6315473;6409847;6542572;7144670;11945621;8593950;9606073;7454040;7950161;8471423;9382616;17777507;9290816;11103834;10537394;10158197;17211067;12654075;11816073;18200023;17719106;12140857;12299660;14106831;12472143;16148645;12952419;13687242;15094831;15404201;15925463;16744856;23009537;17240977;26306842;20695591;19828210;20394650;21641228;21974270;39754393;23956930;36543295;24115733;24440517;24771803;37045429;25252079;25424562;29216999;33515452;41177542;28782073;30499032;42169418;54455962;33985833;37069187;37936568;44600013;40523801;40222860;42035878;59019699;53988802;57631185;54206635;48556250;54034152;50196365;50023882;50676641;54469078;58940014;57999072;104652327;69305874;59281105;64484865;71055020;105132603;82536581;80746661;78159428;155328968;82258738;88779110;104665443;159121405;98580132;130814988;98752615;99232891;100220247;123424879;100700523;169617468;112468150;213328040;117280177;135539885;217798623;123765970;142644293;149214448;163283242;158906089;160418166;166938538;171037848;255112443;138879426;197332747;197813023;197985506;231035235;241046147;388836471;340655316;213168673;277698343;282085618;236234120;252820062;278184178;266410263;262645396;272980418;288093874;364271285;297785515;299297592;305817964;550739270;503150711;336212173;700483458;395145770;570179492;411154179;444203908;449402793;465988735;822999554;524327994;637251703;489054182;498879516;515465458;551164596;585879389;535625814;700963734;587391466;597083107;785614966;635509765;893209430;1004519640;900208361;959669366;806299949;1137043985;1229818874;1897729070;893606701;1059953808;955042917;987933698;1013382176;1014344974;1024679996;1034505330;1121505203;1336473499;1621763103;1810294962;1601602747;1497291468;1953163238;2428063052;1441809714;1959562557;1706508310;1699906650;2922409066;1819682125;1881540399;1907951675;1848649618;1906988877;1942976615;1968425093;2001315874;3927987650;2048850304;2734411980;2156010533;2618796671;2778283213;2939101182;3043412461;3148318024;4436392650;3141716364;7069704014;3555157928;3406414960;3519588775;3817074711;4098987148;5821695674;3730190017;3755638495;7572713206;6878508041;3911401708;6548131324;4779599087;5353208651;4204860837;4774807204;5095111715;7317816668;5919999577;7578109014;6191730485;10966537689;6958791075;6661305139;6926003735;6961572888;9299972552;7249778792;7485828512;10459533032;13150521560;10396591322;8979668041;9558069488;8116262545;8686208912;10128015855;13461016116;11130864572;11286842200;9869918919;11015111292;12111730062;16465496553;20419807191;20573180780;13920363963;15077835433;13587308874;14211351680;25537368465;28059009292;14735607304;15602091057;19973051112;17674332033;16802471457;17095930586;17986181464;18244278400;18556127831;19997934774;23126841354;20885030211;24605526223;21981648981;38975935022;28577226615;27507672837;27798660554;28131715643;28322916178;54053770455;35340208986;29813442737;30337698361;31831537890;41979583755;32404562514;36230459864;33898402043;41113022818;37980960797;39441158042;45108490335;79093983615;42866679192;44011871565;45490556434;46587175204;53813186871;56084899452;69435938996;55306333391;63138869540;69238611029;58660614539;70385523311;60151141098;62742260875;62169236251;65729939933;66302964557;68635022378;70128861907;73339560085;86603579252;77422118839;164025698091;86878550757;88357235626;100400362075;89502427999;233461637087;101893508595;109119520262;129587080094;113966947930;115457474489;118811755637;120829850790;121402875414;199715942001;122320377349;127899176184;128472200808;175422484819;134937986935;141974582463;143468421992;164300669596;165779354465;166924546838;187278912832;216465630851;214367310005;235369823344;191395936594;211013028857;246710931821;223086468192;232778703567;326333923529;236860349903;287182229879;242232726204;366640488839;306275252059;257258364284;522740882910;357835731997;276912569398;278406408927;332703901303;739206513761;355696606190;380146664470;354203459670;642878836069;402408965451;405763246599;414482404786;760244697448;599419192406;919791405467;459946818095;632609175588;479093076107;660478711729;499491090488;690539633300;534170933682;535664773211;679321534849;555318978325;609616470701;756612425121;812577342609;932123093709;734350124140;1140391361918;1150486451395;1851914499176;1096302879899;808172212050;820245651385;874429222881;939039894202;959437908583;1632822993994;1089489912007;1394828835869;978584166595;2611407160589;1033662024170;1069835706893;1695710340243;1687006565490;1164935449026;1289669102465;1343966594841;1490962549261;2524624573431;1972701918372;1542522336190;1628417863435;1682601434931;1694674874266;1897662124057;1747212106252;2717907775442;1813469117083;2103975343228;2661185601526;2012246190765;2048419873488;2103497731063;2859610323292;2729372364413;2198597473196;2234771155919;3338088975953;2454604551491;2508902043867;3091178701093;3868591168272;4466850742256;3170940199625;3237197210456;3225123771121;3311019298366;3377276309197;3441886980518;3560681223335;3759458297017;6921455293747;4048240273002;5490306854006;4060666064253;4871856514057;4689375707410;5536686449149;4927969837609;4433368629115;4707499517063;5325949857012;4963506595358;10092395493372;7502553044771;7878439716688;6396063970746;6536143069487;6408137410081;7820124361270;10727676815892;7070477595383;8249132823254;8268180740398;7621347287588;11115636927144;8481608902117;8108906337255;8494034693368;8750041771663;9122744336525;9140868146178;9361338466724;9396875224473;11103563487809;9671006112421;10289456452370;11359570566104;12804201380827;14414582786175;14890098664114;12932207040233;12944280479568;13478615005464;15179383932638;14691824882971;20226307824334;16115381980956;15730253624843;16102956189705;16590515239372;17231650673780;16602941030623;26282816386291;18502206612902;24030966810292;18537743370651;18758213691197;19067881336894;19960462564791;29535148070856;21649027018474;24163771946931;27983585313465;25876487519801;27834379143682;26410822045697;26422895485032;28170439888435;41102646928668;30422078507814;32705897220328;42141075670540;31833209814548;35733857286682;37192113238571;33834591704403;35105147643525;37039949983553;37295957061848;37605624707545;37826095028091;38718676255988;39028343901685;41609489583265;45812798965405;65266418875599;50040259466732;66702261569453;52287309565498;56004819032117;52833717530729;54593335373467;58592518396249;66155935794496;62255288322362;64539107034876;65667801518951;84666927345277");
                Expecteds.Add("138879426");
                break;
            case (2, true):
                Inputs.Add("35;20;15;25;47;40;62;55;65;95;102;117;150;182;127;219;299;277;309;576");
                Expecteds.Add("62");
                break;
            case (2, false):
                Inputs.Add("42;27;23;19;4;26;31;29;11;49;38;9;45;1;40;46;39;28;44;16;13;5;32;17;50;12;20;6;7;10;22;14;18;60;59;8;19;9;11;15;21;23;39;24;25;16;26;13;29;17;27;28;20;35;30;31;22;32;44;33;38;47;34;36;37;41;55;64;40;42;43;46;39;45;61;48;50;52;65;62;53;67;72;69;70;74;73;98;86;76;79;81;108;84;82;91;160;153;93;253;100;102;105;115;175;166;151;149;142;218;147;152;162;155;161;163;165;173;255;184;191;193;293;195;357;252;295;220;257;404;289;291;322;294;314;415;343;316;318;324;328;512;364;517;379;384;511;636;615;574;472;477;509;644;1021;605;585;608;610;800;634;640;1122;642;1255;692;841;893;981;763;1313;1586;1046;949;986;1057;1062;1094;1444;1300;2393;1193;1250;1244;1274;1475;1282;1405;1334;1533;1585;1604;1744;2467;1712;2986;2043;2193;2255;2048;3250;2338;2287;2437;3711;2443;2526;2494;2518;2556;3382;2938;2990;3527;4149;5876;4660;4993;4050;4724;4091;4236;4241;4303;4781;4625;4730;4805;4880;4937;5432;7280;9717;5074;5494;9027;5928;9505;7577;8966;8141;8286;8291;13021;12377;10162;9535;8544;13907;14235;14459;9610;9685;19222;17294;10506;18637;18683;14040;33262;22526;21882;15718;17751;16427;16432;16835;35110;28293;18079;19050;28322;34586;30339;40277;19295;26520;20191;24546;26224;26933;29758;33090;47837;34506;34914;32553;64269;34178;52868;62471;35885;37374;37129;38270;38345;96649;39486;43841;57636;70898;44737;46415;68387;66887;74155;71307;85211;66731;67059;67467;68438;110684;82300;73014;80622;74230;76615;75399;106217;77831;152678;126074;116044;91152;111624;113474;141117;133618;133790;138366;134198;173276;142668;157699;135905;158453;206696;212520;147244;157237;365198;152014;239548;168983;191305;224942;202776;254591;276458;272564;247092;267408;267816;402014;301121;292651;278573;506956;283149;293142;304481;309251;494139;565215;320997;343319;441547;371759;785529;449868;692756;457367;537740;592400;514500;761848;546389;711265;561722;1157615;585793;571715;846991;1100293;597623;874466;715078;778364;881059;784866;793187;813306;821627;907235;1452774;971867;995107;1157508;1060889;1076222;1118104;1108111;1340086;1370659;1169338;1183416;1375987;1634933;1508265;1419250;1493442;1499944;2407179;1665925;1578053;1889528;1720541;1728862;1879102;1966974;2400975;3213983;2230227;2137111;3457155;2448197;3379046;2352754;2539997;3306915;2993386;5013979;2912692;2919194;3085175;3071495;3165869;3243978;3545027;3298594;5846912;3449403;3607964;3846076;4104085;4940972;4367338;4489865;6764070;4800951;4892751;5265446;5271948;6362095;5831886;6291980;6237364;5984187;6615272;6156670;6315473;6409847;6542572;7144670;11945621;8593950;9606073;7454040;7950161;8471423;9382616;17777507;9290816;11103834;10537394;10158197;17211067;12654075;11816073;18200023;17719106;12140857;12299660;14106831;12472143;16148645;12952419;13687242;15094831;15404201;15925463;16744856;23009537;17240977;26306842;20695591;19828210;20394650;21641228;21974270;39754393;23956930;36543295;24115733;24440517;24771803;37045429;25252079;25424562;29216999;33515452;41177542;28782073;30499032;42169418;54455962;33985833;37069187;37936568;44600013;40523801;40222860;42035878;59019699;53988802;57631185;54206635;48556250;54034152;50196365;50023882;50676641;54469078;58940014;57999072;104652327;69305874;59281105;64484865;71055020;105132603;82536581;80746661;78159428;155328968;82258738;88779110;104665443;159121405;98580132;130814988;98752615;99232891;100220247;123424879;100700523;169617468;112468150;213328040;117280177;135539885;217798623;123765970;142644293;149214448;163283242;158906089;160418166;166938538;171037848;255112443;138879426;197332747;197813023;197985506;231035235;241046147;388836471;340655316;213168673;277698343;282085618;236234120;252820062;278184178;266410263;262645396;272980418;288093874;364271285;297785515;299297592;305817964;550739270;503150711;336212173;700483458;395145770;570179492;411154179;444203908;449402793;465988735;822999554;524327994;637251703;489054182;498879516;515465458;551164596;585879389;535625814;700963734;587391466;597083107;785614966;635509765;893209430;1004519640;900208361;959669366;806299949;1137043985;1229818874;1897729070;893606701;1059953808;955042917;987933698;1013382176;1014344974;1024679996;1034505330;1121505203;1336473499;1621763103;1810294962;1601602747;1497291468;1953163238;2428063052;1441809714;1959562557;1706508310;1699906650;2922409066;1819682125;1881540399;1907951675;1848649618;1906988877;1942976615;1968425093;2001315874;3927987650;2048850304;2734411980;2156010533;2618796671;2778283213;2939101182;3043412461;3148318024;4436392650;3141716364;7069704014;3555157928;3406414960;3519588775;3817074711;4098987148;5821695674;3730190017;3755638495;7572713206;6878508041;3911401708;6548131324;4779599087;5353208651;4204860837;4774807204;5095111715;7317816668;5919999577;7578109014;6191730485;10966537689;6958791075;6661305139;6926003735;6961572888;9299972552;7249778792;7485828512;10459533032;13150521560;10396591322;8979668041;9558069488;8116262545;8686208912;10128015855;13461016116;11130864572;11286842200;9869918919;11015111292;12111730062;16465496553;20419807191;20573180780;13920363963;15077835433;13587308874;14211351680;25537368465;28059009292;14735607304;15602091057;19973051112;17674332033;16802471457;17095930586;17986181464;18244278400;18556127831;19997934774;23126841354;20885030211;24605526223;21981648981;38975935022;28577226615;27507672837;27798660554;28131715643;28322916178;54053770455;35340208986;29813442737;30337698361;31831537890;41979583755;32404562514;36230459864;33898402043;41113022818;37980960797;39441158042;45108490335;79093983615;42866679192;44011871565;45490556434;46587175204;53813186871;56084899452;69435938996;55306333391;63138869540;69238611029;58660614539;70385523311;60151141098;62742260875;62169236251;65729939933;66302964557;68635022378;70128861907;73339560085;86603579252;77422118839;164025698091;86878550757;88357235626;100400362075;89502427999;233461637087;101893508595;109119520262;129587080094;113966947930;115457474489;118811755637;120829850790;121402875414;199715942001;122320377349;127899176184;128472200808;175422484819;134937986935;141974582463;143468421992;164300669596;165779354465;166924546838;187278912832;216465630851;214367310005;235369823344;191395936594;211013028857;246710931821;223086468192;232778703567;326333923529;236860349903;287182229879;242232726204;366640488839;306275252059;257258364284;522740882910;357835731997;276912569398;278406408927;332703901303;739206513761;355696606190;380146664470;354203459670;642878836069;402408965451;405763246599;414482404786;760244697448;599419192406;919791405467;459946818095;632609175588;479093076107;660478711729;499491090488;690539633300;534170933682;535664773211;679321534849;555318978325;609616470701;756612425121;812577342609;932123093709;734350124140;1140391361918;1150486451395;1851914499176;1096302879899;808172212050;820245651385;874429222881;939039894202;959437908583;1632822993994;1089489912007;1394828835869;978584166595;2611407160589;1033662024170;1069835706893;1695710340243;1687006565490;1164935449026;1289669102465;1343966594841;1490962549261;2524624573431;1972701918372;1542522336190;1628417863435;1682601434931;1694674874266;1897662124057;1747212106252;2717907775442;1813469117083;2103975343228;2661185601526;2012246190765;2048419873488;2103497731063;2859610323292;2729372364413;2198597473196;2234771155919;3338088975953;2454604551491;2508902043867;3091178701093;3868591168272;4466850742256;3170940199625;3237197210456;3225123771121;3311019298366;3377276309197;3441886980518;3560681223335;3759458297017;6921455293747;4048240273002;5490306854006;4060666064253;4871856514057;4689375707410;5536686449149;4927969837609;4433368629115;4707499517063;5325949857012;4963506595358;10092395493372;7502553044771;7878439716688;6396063970746;6536143069487;6408137410081;7820124361270;10727676815892;7070477595383;8249132823254;8268180740398;7621347287588;11115636927144;8481608902117;8108906337255;8494034693368;8750041771663;9122744336525;9140868146178;9361338466724;9396875224473;11103563487809;9671006112421;10289456452370;11359570566104;12804201380827;14414582786175;14890098664114;12932207040233;12944280479568;13478615005464;15179383932638;14691824882971;20226307824334;16115381980956;15730253624843;16102956189705;16590515239372;17231650673780;16602941030623;26282816386291;18502206612902;24030966810292;18537743370651;18758213691197;19067881336894;19960462564791;29535148070856;21649027018474;24163771946931;27983585313465;25876487519801;27834379143682;26410822045697;26422895485032;28170439888435;41102646928668;30422078507814;32705897220328;42141075670540;31833209814548;35733857286682;37192113238571;33834591704403;35105147643525;37039949983553;37295957061848;37605624707545;37826095028091;38718676255988;39028343901685;41609489583265;45812798965405;65266418875599;50040259466732;66702261569453;52287309565498;56004819032117;52833717530729;54593335373467;58592518396249;66155935794496;62255288322362;64539107034876;65667801518951;84666927345277");
                Expecteds.Add("23761694");
                break;
        }
    }
}
