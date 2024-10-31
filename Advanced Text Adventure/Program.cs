﻿using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
namespace Advanced_Text_Adventure
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(int key);

        public static readonly string dataPath = Directory.GetCurrentDirectory() + "/Data/";
        static readonly int[] inputIds = [68, 70, 74, 75];
        static readonly int[] menuInputIds = [38, 40, 13, 27];
        static readonly int height = 15;
        static readonly string[] menuOptions = ["Play", "Import", "Settings", "Quit"];

        public static bool[] menuInputsDown = [false, false, false, false];
        public static bool[] menuInputs = [false, false, false, false];
        public static bool[] inputsDown = [false, false, false, false];
        public static bool[] inputs = [false, false, false, false];
        public static Stopwatch stopwatch = new();
        public static float timer;
        public static List<int> noteTimes = [8276, 8621, 8966, 9224, 9310, 9483, 9828, 10000, 10086, 10172, 10345, 10517, 10690, 10862, 11034, 11379, 11552, 11724, 11897, 12069, 12241, 12414, 12759, 12931, 13103, 13276, 13448, 13621, 13793, 14138, 14310, 14483, 14655, 14828, 15000, 15172, 15517, 15690, 15862, 16034, 16207, 16379, 16552, 16897, 17069, 17241, 17414, 17586, 17759, 17931, 18276, 18448, 18621, 18793, 18966, 19138, 19310, 19655, 20000, 20259, 20345, 20517, 20862, 21034, 21121, 21207, 21379, 21466, 21552, 21638, 21724, 21810, 21897, 21983, 22328, 22586, 22931, 23103, 23103, 23448, 23707, 23966, 24310, 24483, 24483, 24828, 25086, 25345, 25690, 25862, 25862, 26207, 26379, 26379, 26552, 26724, 26897, 26897, 27069, 27241, 27241, 27414, 27845, 28103, 28448, 28534, 28621, 29224, 29483, 29828, 29914, 30000, 30345, 30603, 30862, 31207, 31293, 31379, 31724, 31810, 31897, 31983, 32069, 32155, 32241, 32328, 32414, 32500, 32586, 32672, 32759, 32845, 32931, 33017, 33103, 33276, 33448, 33621, 33793, 33966, 34138, 34310, 34483, 34655, 34828, 35000, 35172, 35345, 35517, 35690, 35862, 35862, 36207, 36379, 36552, 36724, 36897, 37069, 37241, 37414, 37586, 37759, 37931, 38103, 38276, 38448, 38621, 38621, 38966, 39138, 39310, 39483, 39655, 39828, 40000, 40172, 40345, 40517, 40690, 40862, 41034, 41379, 41379, 41724, 41983, 42069, 42241, 42414, 42586, 42759, 42845, 42931, 43103, 43276, 43362, 43448, 43793, 44138, 44483, 44741, 44828, 45000, 45172, 45259, 45345, 45517, 45690, 45862, 46034, 46207, 46379, 46552, 46897, 47241, 47500, 47586, 47759, 47759, 47931, 48017, 48103, 48276, 48362, 48448, 48621, 48793, 48966, 49138, 49310, 49655, 50000, 50000, 50259, 50345, 50517, 50690, 50776, 50862, 51034, 51207, 51379, 51552, 51724, 51897, 52069, 52241, 52414, 53793, 54310, 54483, 54828, 55172, 55259, 55345, 55431, 55517, 55603, 55690, 55776, 55948, 56034, 56121, 56207, 56293, 56379, 56466, 56552, 56638, 56724, 56810, 56897, 57155, 57414, 57586, 57759, 57931, 58103, 58276, 58534, 58793, 58966, 59052, 59138, 59310, 59397, 59483, 59655, 59914, 60172, 60259, 60345, 60517, 60690, 60862, 61034, 61207, 61379, 61552, 61724, 61897, 62069, 62241, 62328, 62414, 62500, 62586, 62759, 62845, 62931, 63017, 63103, 63276, 63362, 63448, 63793, 63879, 63966, 64052, 64310, 64483, 64569, 64655, 64828, 65172, 65517, 65862, 66207, 66379, 66552, 66724, 66897, 67069, 67241, 67414, 67500, 67586, 67759, 67931, 68103, 68276, 68448, 68621, 68793, 68966, 69138, 69310, 69483, 69655, 69828, 70000, 70172, 70345, 70517, 70690, 70862, 71034, 71207, 71379, 71552, 71724, 71810, 71897, 71983, 72069, 72155, 72241, 72328, 72414, 72500, 72586, 72672, 72759, 72845, 72931, 73017, 73103, 73190, 73276, 73362, 73448, 73534, 73621, 73707, 73793, 73879, 73966, 74052, 74138, 74224, 74310, 74397, 74483, 74569, 74655, 74741, 74828, 74914, 75000, 75086, 75172, 75259, 75345, 75431, 75517, 75603, 75690, 75776, 75862, 76121, 76379, 76724, 76897, 77241, 77586, 77931, 78103, 78276, 78621, 78966, 79310, 79310, 79655, 79655, 80000, 80345, 80690, 80862, 80948, 81034, 81379, 81552, 81638, 81724, 81897, 82069, 82155, 82241, 82414, 82586, 82759, 83103, 83103, 83448, 83793, 83793, 84138, 84483, 84828, 84828, 85172, 85172, 85517, 85862, 86207, 86552, 86897, 87069, 87241, 87328, 87414, 87586, 87672, 87759, 87845, 87931, 88103, 88276, 88966, 89483, 90345, 90862, 91207, 91724, 91897, 92241, 92414, 92586, 92672, 92759, 92931, 93103, 93190, 93276, 93448, 93621, 93793, 94483, 95000, 95862, 96379, 96724, 97241, 97414, 97759, 97931, 98103, 98190, 98276, 98448, 98621, 98793, 98879, 98966, 99138, 99310, 99655, 100172, 100690, 101034, 101552, 102069, 102414, 102931, 103448, 103793, 104138, 104483, 104828, 105000, 105086, 105172, 105517, 105603, 105690, 106034, 106121, 106207, 106379, 106466, 106552, 106897, 106983, 107069, 107414, 107586, 107586, 110345, 110431, 110517, 110603, 110862, 111207, 111379, 111552, 111724, 111810, 111897, 111983, 112069, 112155, 112241, 112328, 112414, 112500, 112586, 112672, 112759, 112845, 112931, 113017, 113103, 113190, 113276, 113362, 113448, 113534, 113621, 113966, 113966, 114138, 114138, 114483, 114569, 114655, 114741, 114828, 114914, 115000, 115086, 115172, 115259, 115345, 115431, 115517, 115603, 115690, 115776, 115862, 115948, 116034, 116121, 116379, 116724, 116897, 117069, 117241, 117328, 117414, 117500, 117586, 117672, 117759, 117845, 117931, 118017, 118103, 118190, 118276, 118362, 118448, 118534, 118621, 118707, 118793, 118879, 118966, 119052, 119138, 119483, 119483, 119655, 119655, 120000, 120172, 120345, 120517, 120690, 120862, 121034, 121379, 121466, 121552, 121638, 121897, 122241, 122414, 122586, 122759, 122845, 122931, 123017, 123103, 123190, 123276, 123362, 123448, 123534, 123621, 123707, 123793, 123879, 123966, 124052, 124138, 124224, 124310, 124397, 124483, 124569, 124655, 125000, 125000, 125172, 125172, 125517, 125603, 125690, 125776, 125862, 125948, 126034, 126121, 126207, 126293, 126379, 126466, 126552, 126638, 126724, 126810, 126897, 126983, 127069, 127155, 127414, 127759, 127931, 128103, 128276, 128362, 128448, 128534, 128621, 128707, 128793, 128879, 128966, 129052, 129138, 129224, 129310, 129397, 129483, 129569, 129655, 129741, 129828, 129914, 130000, 130086, 130172, 130517, 130517, 130690, 130690, 131034, 131207, 131379, 131552, 131724, 131897, 132069, 132414, 132586, 132672, 132759, 132931, 133017, 133103, 133190, 133276, 133621, 133793, 133793, 134138, 134224, 134310, 134483, 134828, 134828, 135000, 135172, 135345, 135431, 135517, 135690, 135776, 135862, 136207, 136293, 136379, 136466, 136552, 136724, 136897, 137069, 137241, 137414, 137586, 137759, 137931, 138103, 138190, 138276, 138448, 138534, 138621, 138707, 138793, 138966, 139138, 139310, 139397, 139483, 139569, 139655, 139828, 140000, 140345, 140345, 140517, 140690, 140776, 140862, 140948, 141034, 141207, 141379, 141466, 141552, 141638, 141724, 141897, 142069, 142241, 142414, 142586, 142759, 142931, 143103, 143276, 143448, 143534, 143707, 143793, 143879, 144052, 144138, 144310, 144655, 145172, 145345, 145517, 145603, 145690, 145776, 145862, 145862, 146034, 146121, 146207, 146293, 146379, 146466, 146552, 146724, 146897, 146983, 147069, 147241, 147241, 147414, 147414, 147586, 147759, 147931, 148103, 148276, 148448, 148621, 148793, 148966, 149052, 149138, 149224, 149310, 149397, 149483, 149569, 149655, 149655, 149828, 149828, 150172, 150172, 150345, 150431, 150517, 150603, 150690, 150776, 150862, 151034, 151121, 151207, 151293, 151379, 151379, 151552, 151552, 151724, 151810, 151897, 151983, 152069, 152155, 152241, 152586, 152672, 152759, 152931, 153103, 153276, 153448, 153621, 153793, 153966, 154138, 154310, 154483, 154655, 154741, 154828, 155000, 155172, 155345, 155517, 155690, 155776, 155862, 156034, 156207, 156379, 156552, 156724, 156810, 156897, 157069, 157241, 157414, 157586, 157759, 157845, 157931, 158103, 158276, 158448, 158621, 158793, 158879, 158966, 159138, 159224, 159310, 159483, 159569, 159655, 160000, 160172, 160259, 160345, 160517, 160690, 160862, 161034, 161207, 161293, 161379, 161552, 161724, 161897, 162069, 162241, 162328, 162414, 162586, 162759, 162931, 163103, 163276, 163362, 163448, 163621, 163793, 163966, 164138, 164310, 164397, 164483, 164655, 164828, 165000, 165172, 165345, 165517, 165690, 165862, 166034, 166121, 166207, 166293, 166379, 166466, 166552, 166897, 167069, 167241, 167414, 167500, 167586, 167672, 167759, 167845, 167931, 168276, 168448, 168621, 168793, 168879, 168966, 169052, 169138, 169224, 169310, 169655, 169828, 170000, 170172, 170259, 170345, 170431, 170517, 170603, 170690, 171034, 171207, 171379, 171552, 171638, 171724, 171810, 171897, 171983, 172069, 172414, 172586, 172759, 172931, 173017, 173103, 173190, 173276, 173362, 173448, 173793];
        public static List<int> noteLanes = [0, 1, 2, 1, 3, 0, 1, 2, 3, 0, 1, 2, 3, 2, 1, 2, 0, 2, 0, 3, 0, 2, 3, 1, 3, 0, 3, 0, 1, 3, 0, 2, 3, 0, 3, 2, 1, 3, 0, 3, 1, 3, 0, 3, 1, 2, 3, 1, 2, 3, 2, 1, 0, 2, 0, 1, 0, 2, 3, 2, 1, 0, 3, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 2, 1, 1, 3, 0, 0, 1, 2, 2, 3, 0, 1, 0, 3, 2, 1, 3, 0, 1, 2, 0, 2, 3, 1, 0, 3, 2, 1, 2, 1, 0, 1, 0, 1, 2, 3, 2, 3, 1, 1, 2, 3, 2, 3, 3, 2, 1, 3, 2, 1, 3, 2, 1, 3, 2, 1, 0, 1, 2, 0, 3, 1, 2, 3, 1, 2, 0, 2, 1, 0, 2, 1, 2, 1, 0, 1, 0, 2, 1, 3, 2, 1, 3, 1, 2, 3, 1, 3, 2, 1, 2, 3, 1, 2, 0, 3, 2, 0, 3, 0, 2, 3, 0, 3, 2, 0, 3, 2, 0, 1, 0, 3, 0, 1, 3, 0, 1, 3, 1, 0, 1, 3, 1, 0, 2, 3, 1, 3, 1, 2, 0, 1, 2, 1, 2, 1, 0, 2, 2, 1, 1, 0, 3, 2, 0, 1, 2, 0, 1, 2, 3, 2, 3, 2, 1, 0, 3, 1, 0, 2, 1, 0, 2, 1, 2, 1, 0, 2, 1, 2, 3, 2, 1, 0, 3, 0, 2, 0, 2, 1, 3, 2, 0, 3, 1, 2, 3, 1, 2, 0, 1, 3, 0, 2, 3, 1, 2, 1, 2, 0, 1, 0, 1, 2, 1, 2, 3, 2, 0, 1, 0, 2, 3, 1, 2, 1, 0, 3, 0, 1, 2, 3, 1, 0, 2, 3, 1, 2, 1, 0, 2, 3, 2, 0, 1, 2, 3, 0, 1, 2, 2, 3, 1, 0, 1, 0, 1, 2, 3, 0, 2, 3, 0, 1, 0, 1, 0, 1, 0, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2, 1, 0, 1, 0, 1, 0, 1, 0, 2, 3, 2, 3, 2, 3, 2, 3, 0, 2, 0, 2, 1, 3, 1, 3, 2, 0, 2, 0, 3, 1, 3, 1, 2, 1, 2, 1, 3, 0, 3, 0, 1, 0, 1, 0, 2, 3, 2, 3, 1, 3, 0, 2, 1, 3, 0, 1, 2, 0, 3, 1, 2, 0, 1, 2, 3, 2, 1, 1, 0, 3, 2, 2, 0, 1, 0, 0, 1, 0, 2, 1, 2, 0, 0, 1, 0, 2, 0, 1, 2, 0, 1, 2, 3, 0, 2, 3, 0, 3, 1, 1, 2, 1, 3, 3, 1, 3, 3, 2, 1, 3, 3, 1, 3, 1, 3, 1, 3, 2, 0, 2, 0, 2, 0, 1, 2, 2, 0, 3, 3, 0, 3, 2, 3, 2, 1, 3, 2, 1, 0, 3, 1, 0, 2, 1, 1, 3, 0, 0, 3, 0, 1, 0, 1, 2, 0, 1, 2, 0, 1, 2, 3, 0, 3, 0, 3, 0, 3, 0, 3, 0, 2, 0, 3, 1, 2, 3, 0, 1, 0, 3, 2, 0, 3, 1, 3, 0, 2, 0, 3, 1, 2, 3, 0, 0, 3, 2, 1, 2, 2, 1, 3, 2, 0, 2, 0, 3, 1, 3, 1, 2, 1, 2, 1, 3, 0, 3, 0, 2, 3, 2, 1, 0, 1, 2, 1, 3, 1, 0, 3, 1, 2, 0, 3, 1, 2, 0, 1, 0, 3, 2, 1, 3, 0, 2, 3, 0, 1, 2, 1, 1, 2, 0, 1, 3, 1, 3, 0, 2, 0, 2, 1, 2, 1, 2, 0, 3, 0, 3, 1, 0, 1, 2, 3, 2, 1, 0, 2, 3, 2, 3, 3, 0, 0, 2, 2, 1, 3, 0, 1, 2, 1, 1, 2, 0, 1, 3, 1, 3, 0, 2, 0, 2, 1, 2, 1, 2, 0, 3, 0, 3, 1, 0, 1, 2, 3, 2, 1, 0, 2, 2, 3, 0, 2, 1, 3, 0, 2, 1, 3, 2, 3, 0, 1, 2, 0, 3, 1, 0, 3, 2, 1, 2, 2, 1, 3, 2, 0, 2, 0, 3, 1, 3, 1, 2, 1, 2, 1, 3, 0, 3, 0, 2, 3, 2, 1, 0, 1, 2, 3, 1, 1, 0, 0, 0, 3, 3, 1, 1, 2, 0, 2, 3, 1, 3, 0, 2, 3, 1, 0, 2, 3, 1, 3, 2, 3, 2, 3, 1, 0, 2, 3, 1, 2, 3, 0, 3, 0, 1, 2, 0, 1, 0, 1, 2, 0, 2, 1, 3, 1, 0, 2, 0, 3, 1, 0, 2, 3, 3, 1, 0, 1, 0, 2, 1, 0, 1, 0, 2, 3, 0, 1, 0, 2, 1, 3, 2, 3, 2, 0, 2, 3, 2, 3, 2, 1, 3, 1, 0, 2, 0, 0, 1, 3, 3, 2, 1, 2, 1, 2, 3, 1, 2, 3, 1, 0, 2, 3, 1, 3, 0, 3, 2, 1, 2, 0, 2, 2, 3, 1, 0, 2, 1, 0, 1, 2, 0, 2, 3, 1, 3, 2, 0, 2, 0, 1, 3, 1, 0, 2, 3, 1, 3, 0, 2, 3, 0, 2, 3, 1, 0, 2, 1, 0, 2, 3, 1, 0, 2, 0, 3, 0, 1, 3, 2, 0, 3, 1, 2, 3, 2, 3, 2, 1, 3, 1, 0, 0, 2, 3, 1, 0, 2, 1, 3, 1, 0, 2, 0, 1, 2, 0, 3, 2, 1, 3, 2, 1, 3, 1, 0, 2, 0, 1, 3, 0, 2, 3, 1, 3, 0, 2, 0, 1, 3, 3, 0, 1, 2, 0, 1, 3, 0, 3, 2, 1, 3, 2, 1, 3, 0, 1, 2, 0, 1, 3, 0, 3, 2, 1, 3, 2, 0, 3, 0, 1, 2, 0, 1, 3, 0, 3, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 0, 3, 0, 3, 0, 3, 0, 3, 0, 3, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 2, 1, 0, 2, 3, 1, 0, 2, 1, 2, 0, 1, 3, 2, 1, 0, 3, 3];
        public static List<int> noteHolds = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1380, 0, 0, 0, 0, 0, 0, 1379, 0, 0, 0, 0, 0, 0, 1379, 0, 0, 0, 0, 0, 0, 1380, 0, 0, 0, 0, 0, 0, 1379, 0, 0, 0, 0, 0, 0, 1379, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 173, 0, 258, 173, 172, 0, 172, 173, 0, 172, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 345, 0, 173, 2241, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2759, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2758, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2759, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 258, 259, 0, 0, 0, 0, 0, 0, 0, 259, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 172, 0, 172, 0, 172, 0, 173, 0, 0, 173, 0, 172, 0, 172, 0, 172, 0, 172, 0, 173, 0, 173, 0, 172, 0, 172, 0, 172, 0, 173, 0, 173, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 344, 4138, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 173, 172, 0, 173, 172, 172, 0, 173, 172, 173, 4138, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4138, 0, 0, 0, 0, 0, 0, 0, 0, 172, 173, 0, 172, 172, 173, 0, 172, 173, 172, 4138, 0, 0, 0, 0, 0, 0, 0, 0, 172, 173, 0, 172, 173, 172, 173, 0, 172, 172, 345, 517, 518, 344, 518, 517, 345, 517, 517, 0, 0, 0, 0, 344, 0, 0, 518, 0, 0, 517, 0, 0, 345, 0, 0, 517, 0, 0, 517, 0, 0, 0, 258, 0, 0, 259, 172, 172, 345, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 862, 0, 0, 0, 0, 172, 0, 173, 0, 172, 0, 172, 0, 173, 0, 172, 0, 173, 0, 172, 0, 259, 0, 0, 258, 173, 173, 344, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 862, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 345, 259, 0, 0, 259, 172, 173, 345, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 862, 0, 0, 0, 0, 173, 0, 172, 0, 172, 0, 173, 0, 172, 0, 173, 0, 172, 0, 173, 0, 258, 0, 0, 259, 172, 172, 345, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 862, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 345, 345, 0, 0, 344, 0, 0, 173, 0, 345, 517, 0, 0, 172, 0, 173, 0, 172, 0, 172, 345, 0, 0, 173, 517, 0, 0, 172, 0, 173, 0, 0, 0, 0, 0, 0, 0, 0, 0, 345, 0, 0, 345, 0, 0, 172, 0, 345, 0, 517, 0, 0, 0, 0, 173, 172, 0, 172, 0, 173, 344, 0, 0, 0, 173, 517, 0, 0, 0, 0, 173, 172, 0, 0, 0, 0, 0, 0, 0, 0, 345, 0, 0, 345, 0, 0, 172, 345, 517, 173, 172, 0, 0, 0, 0, 172, 0, 173, 0, 345, 0, 0, 0, 172, 517, 0, 0, 0, 173, 0, 172, 0, 173, 172, 172, 173, 172, 173, 172, 173, 344, 0, 0, 0, 345, 0, 0, 0, 173, 0, 344, 0, 518, 0, 0, 0, 0, 0, 172, 0, 172, 0, 0, 0, 0, 173, 0, 172, 0, 345, 0, 0, 0, 172, 0, 518, 0, 0, 172, 172, 173, 172, 173, 172, 173, 172, 172, 0, 345, 0, 0, 344, 0, 345, 0, 345, 0, 0, 345, 0, 345, 0, 345, 0, 0, 344, 0, 345, 0, 345, 0, 0, 345, 0, 345, 0, 345, 0, 0, 344, 0, 0, 345, 0, 0, 0, 345, 0, 0, 345, 0, 344, 0, 345, 0, 0, 345, 0, 345, 0, 345, 0, 0, 345, 0, 344, 0, 345, 0, 0, 345, 0, 345, 0, 345, 0, 0, 345, 0, 344, 0, 345, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2759];
        public static int[] judgements = [0, 0, 0, 0];
        public static int noteToSpawn = 0;
        public static List<Note> notes = [];
        public static List<Note> removeNotes = [];
        public static float accuracy = 100;
        public static int combo = 0;
        public static int notesDestroyed = 0;
        public static int hitDelta;
        public static float scrollSpeed = 1.8f;
        public static List<Song> songs;
        public static int selectedSong;
        public static int selectedDifficulty;
        public static int menuPos = 0;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            LoadSongs();
            Thread getInputs = new(new ThreadStart(GetMenuInputs));
            getInputs.Start();
            while (true)
            {
            home:
                Console.Clear();
                menuPos = 0;
                bool keyPressed = true;
                while (true)
                {
                    if (menuInputsDown[0])
                    {
                        menuPos = (int)MathF.Max(menuPos - 1, 0);
                        keyPressed = true;
                    }
                    else if (menuInputsDown[1])
                    {
                        menuPos = (int)MathF.Min(menuPos + 1, menuOptions.Length - 1);
                        keyPressed = true;
                    }
                    else if (menuInputsDown[2])
                        break;
                    if (keyPressed)
                    {
                        for (int o = 0; o < menuOptions.Length; o++)
                        {
                            Console.SetCursorPosition(0, o);
                            if (o == menuPos)
                                Console.ForegroundColor = ConsoleColor.White;
                            else
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(menuOptions[o]);
                        }
                        keyPressed = false;
                    }
                }
                Console.ForegroundColor= ConsoleColor.White;
                menuInputsDown[2] = false;
                Console.ReadLine();
                Console.Clear();
                switch (menuPos)
                {
                    case 0:
                        menuPos = 0;
                        int diffs = 0;
                        while (true)
                        {
                            keyPressed = true;
                            bool play = false;
                            while (true)
                            {
                                if (menuInputsDown[0])
                                {
                                    menuPos = (int)MathF.Max(menuPos - 1, 0);
                                    keyPressed = true;
                                }
                                else if (menuInputsDown[1])
                                {
                                    menuPos = (int)MathF.Min(menuPos + 1, songs.Count - 1);
                                    keyPressed = true;
                                }
                                else if (menuInputsDown[2])
                                {
                                    keyPressed = true;
                                    break;
                                }
                                else if (menuInputsDown[3])
                                    goto home;
                                if (keyPressed)
                                {
                                    Console.SetCursorPosition(0, 0);
                                    if (songs[menuPos].imagePath is not null)
                                        ImageGenerator.DrawImage(songs[menuPos].imagePath, 9);
                                    for (int s = 0; s < 7; s++)
                                    {
                                        Console.SetCursorPosition(19, s + 1);
                                        ClearLine();
                                        if (menuPos + (s - 3) >= 0 && menuPos + (s - 3) <= songs.Count - 1)
                                        {
                                            Console.ForegroundColor = s switch
                                            {
                                                3 => ConsoleColor.White,
                                                2 or 4 => ConsoleColor.Gray,
                                                _ => ConsoleColor.DarkGray,
                                            };
                                            if (s != 3)
                                                Console.Write($"  {songs[menuPos + (s - 3)].name} ({songs[menuPos + (s - 3)].artist})");
                                            else
                                                Console.Write($"> {songs[menuPos].name} ({songs[menuPos].artist})");
                                        }
                                    }
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    Console.SetCursorPosition(0, 10);
                                    for (int d = 0; d < songs[menuPos].chartPaths.Length; d++)
                                    {
                                        ClearLine();
                                        Console.WriteLine(songs[menuPos].chartNames[d]);
                                    }
                                    for (int e = 0; e < diffs - songs[menuPos].chartPaths.Length; e++)
                                        ClearLine(true);
                                    diffs = songs[menuPos].chartPaths.Length;
                                    keyPressed = false;
                                }
                            }
                            selectedDifficulty = 0;
                            menuInputsDown[2] = false;
                            while (true)
                            {
                                if (menuInputsDown[0])
                                {
                                    selectedDifficulty = (int)MathF.Max(selectedDifficulty - 1, 0);
                                    keyPressed = true;
                                }
                                else if (menuInputsDown[1])
                                {
                                    selectedDifficulty = (int)MathF.Min(selectedDifficulty + 1, songs[menuPos].chartPaths.Length - 1);
                                    keyPressed = true;
                                }
                                else if (menuInputsDown[2])
                                {
                                    play = true;
                                    break;
                                }
                                else if (menuInputsDown[3])
                                {
                                    menuInputsDown[3] = false;
                                    break;
                                }
                                if (keyPressed)
                                {
                                    for (int d = 0; d < songs[menuPos].chartPaths.Length; d++)
                                    {
                                        Console.SetCursorPosition(0, d + 10);
                                        if (d == selectedDifficulty)
                                            Console.ForegroundColor = ConsoleColor.White;
                                        else
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.Write(songs[menuPos].chartNames[d]);
                                    }
                                    keyPressed = false;
                                }
                            }
                            if (play)
                            {
                                selectedSong = menuPos;
                                ManiaConverter.ReadData();
                                DrawStats();
                                WMPLib.WindowsMediaPlayer wplayer = new();
                                wplayer.URL = songs[selectedSong].path + "/" + songs[selectedSong].audioFile;
                                wplayer.settings.volume = 15;
                                wplayer.controls.play();
                                wplayer.controls.currentPosition = 0;
                                stopwatch.Start();
                                while (true)
                                {
                                    timer = stopwatch.ElapsedMilliseconds - 180;
                                    for (int i = 0; i < inputIds.Length; i++)
                                    {
                                        if (GetAsyncKeyState(inputIds[i]) != 0)
                                        {
                                            inputsDown[i] = !inputs[i];
                                            inputs[i] = true;
                                        }
                                        else
                                            inputsDown[i] = inputs[i] = false;
                                    }
                                    if (noteToSpawn < noteTimes.Count && noteTimes[noteToSpawn] - (50 / scrollSpeed) * height - 50 < timer)
                                    {
                                        notes.Add(new(noteTimes[noteToSpawn], noteLanes[noteToSpawn], noteHolds[noteToSpawn]));
                                        noteToSpawn++;
                                    }
                                    List<string> noteRender = [];
                                    for (int l = 0; l < height + 3; l++)
                                        noteRender.Add("                ");
                                    noteRender[height] = "";
                                    foreach (bool input in inputs)
                                    {
                                        if (input)
                                            noteRender[height] += "  ▓▓";
                                        else
                                            noteRender[height] += "  ░░";
                                    }
                                    foreach (Note note in notes)
                                    {
                                        Vector2 pos = new(note.lane * 4 + 2, height - (int)MathF.Min((note.time - timer) / (50 / scrollSpeed), height));
                                        if (note.holding)
                                        {
                                            pos.Y = height;
                                            if (!inputs[note.lane] || note.time + note.holdTime + 125 < timer)
                                                removeNotes.Add(note);
                                        }
                                        else if (note.time + 125 < timer)
                                            removeNotes.Add(note);
                                        else if (inputsDown[note.lane])
                                        {
                                            bool first = true;
                                            foreach (Note n in notes)
                                            {
                                                if (n.lane == note.lane && n.time < note.time)
                                                {
                                                    first = false;
                                                    break;
                                                }
                                            }
                                            if (first && MathF.Abs(note.time - timer) <= 250)
                                            {
                                                if (note.holdTime == 0)
                                                    removeNotes.Add(note);
                                                else
                                                {
                                                    note.holding = true;
                                                    JudgeTiming(timer - note.time);
                                                }
                                            }
                                        }
                                        else if (pos.Y < height + 3)
                                            noteRender[(int)pos.Y] = ReplaceChar(noteRender[(int)pos.Y], (int)pos.X, "██");
                                        if (note.holdTime > 0)
                                        {
                                            int holdEnd = height - (int)MathF.Min((note.time + note.holdTime - timer) / (50 / scrollSpeed), height);
                                            int yAt = (int)pos.Y;
                                            while (yAt > 0)
                                            {
                                                yAt--;
                                                if (((yAt >= holdEnd || yAt == pos.Y - 1) && yAt < height + 3))
                                                    noteRender[yAt] = ReplaceChar(noteRender[yAt], (int)pos.X, "░░");
                                                else break;
                                            }
                                        }
                                    }
                                    Console.SetCursorPosition(0, 0);
                                    Console.Write(string.Join("\r\n", [.. noteRender]));
                                    foreach (Note note in removeNotes)
                                    {
                                        float delta;
                                        if (!note.holding)
                                            delta = timer - note.time;
                                        else
                                            delta = timer - (note.time + note.holdTime);
                                        JudgeTiming(delta, note.holding);
                                        notes.Remove(note);
                                    }
                                    removeNotes = [];
                                }
                            }
                        }
                    case 1:
                        ManiaConverter.ImportSong();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static async void GetMenuInputs()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    for (int i = 0; i < menuInputIds.Length; i++)
                    {
                        if (GetAsyncKeyState(menuInputIds[i]) != 0)
                        {
                            menuInputsDown[i] = !menuInputs[i];
                            menuInputs[i] = true;
                        }
                        else
                            menuInputsDown[i] = menuInputs[i] = false;
                    }
                }
            });
        }

        public static void JudgeTiming(float delta, bool holdEnd = false)
        {
            float abs = MathF.Abs(delta);
            if (abs >= 100)
            {
                judgements[3]++;
                combo = 0;
                hitDelta = (int)delta;
            }
            else if (abs >= 80)
            {
                judgements[2]++;
                combo++;
                hitDelta = (int)delta;
            }
            else if (abs >= 50 && !holdEnd)
            {
                judgements[1]++;
                combo++;
                hitDelta = (int)delta;
            }
            else
            {
                judgements[0]++;
                combo++;
                hitDelta = 0;
            }
            notesDestroyed++;
            accuracy = MathF.Floor((judgements[0] + judgements[1] * 0.5f + judgements[2] * 0.25f) / notesDestroyed * 10000) / 100;
            DrawStats();
        }

        public static void DrawStats()
        {
            Console.SetCursorPosition(18, 2);
            Write("Accuracy: " + accuracy + "%", 5);
            Console.SetCursorPosition(18, 4);
            Write("Combo: " + combo, 4);
            Console.SetCursorPosition(18, 6);
            Write("Perfect x " + judgements[0], 4, ConsoleColor.Yellow);
            Console.SetCursorPosition(18, 8);
            Write("Good x " + judgements[1], 4, ConsoleColor.Green);
            Console.SetCursorPosition(18, 10);
            Write("Bad x " + judgements[2], 4, ConsoleColor.Red);
            Console.SetCursorPosition(18, 12);
            Write("Miss x " + judgements[3], 4, ConsoleColor.DarkRed);
            Console.SetCursorPosition(18, height);
            if (hitDelta == 0)
                Console.Write("     ");
            else if (hitDelta < 0)
                Write("Early", 4, ConsoleColor.Blue);
            else
                Write("Late", 4, ConsoleColor.Red);
        }

        static void Write(string text, int clearSpace = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            foreach (char c in text) 
                Console.Write(c);
            for (int s = 0; s <= clearSpace; s++)
                Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string ReplaceChar(string text, int index, string replacement)
        {
            string output = "";
            for (int i = 0; i < index; i++)
                output += text[i];
            output += replacement;
            for (int i = index + replacement.Length; i < text.Length; i++)
                output += text[i];
            return output;
        }

        public static string InputString(List<string> options)
        {
            string output = "";
            while (!options.Contains(output))
                output = Console.ReadLine().ToLower();
            return output;
        }

        public static int InputInt(int min, int max)
        {
            int output = min - 1;
            while (output < min || output > max)
                int.TryParse(Console.ReadLine(), out output);
            return output;
        }

        public static void LoadSongs()
        {
            songs = [];
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);
            string[] songPaths = Directory.GetDirectories(dataPath);
            for (int i = 0; i < songPaths.Length; i++)
            {
                List<string> songData = [.. File.ReadAllText(Directory.GetFiles(songPaths[i], "*.osu")[0]).Split("\r\n")];
                int metadataIndex = songData.IndexOf("[Metadata]");
                string songName = songData[metadataIndex + 1].Split(":")[1];
                string artistName = songData[metadataIndex + 3].Split(":")[1];
                string audioFile = songData[songData.IndexOf("[General]") + 1].Split(": ")[1];
                string[] difficultyPaths = Directory.GetFiles(songPaths[i], "*.osu");
                string[] difficultyNames = new string[difficultyPaths.Length];
                for (int d = 0; d < difficultyPaths.Length; d++)
                {
                    List<string> diffData = [.. File.ReadAllText(difficultyPaths[d]).Split("\r\n")];
                    difficultyNames[d] = diffData[diffData.IndexOf("[Metadata]") + 6].Split(":")[1];
                }
                string imageName = "none";
                if (songData[songData.IndexOf("[Events]") + 2].Split("\"").Length == 3)
                    imageName = songData[songData.IndexOf("[Events]") + 2].Split("\"")[1];
                string? imagePath = null;
                if (File.Exists(songPaths[i] + "/" + imageName))
                    imagePath = songPaths[i] + "/" + imageName;
                songs.Add(new(songName, artistName, songPaths[i], audioFile, difficultyPaths, difficultyNames, imagePath));
            }
        }

        public static void ClearLine(bool nextLine = false)
        {
            Vector2 startPos = new(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top);
            Console.Write(new string(' ', Console.WindowWidth - (int)startPos.X));
            if (!nextLine)
                Console.SetCursorPosition((int)startPos.X, (int)startPos.Y);
            else 
                Console.SetCursorPosition((int)startPos.X, (int)startPos.Y + 1);
        }
    }

    class Note(int time, int lane, int holdTime, bool holding = false)
    {
        public int time = time;
        public int lane = lane;
        public int holdTime = holdTime;
        public bool holding = holding;
    }

    class Song(string name, string artist, string path, string audioFile, string[] chartPaths, string[] chartNames, string? imagePath)
    {
        public string name = name;
        public string artist = artist;
        public string path = path;
        public string audioFile = audioFile;
        public string[] chartPaths = chartPaths;
        public string[] chartNames = chartNames;
        public string? imagePath = imagePath;
    }
}
