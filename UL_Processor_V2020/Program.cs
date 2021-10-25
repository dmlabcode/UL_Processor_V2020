using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UL_Processor_V2020
{
     
    class Program
    {
        static String szVersion = "";
         

        static void Main( string[] arguments)
        {
           // Utilities.mergeDayFiles("E:\\StarFish_2021\\SYNC\\ONSETS", "21_GR0_22_061621_1596558875.CSV", "DAYONSETS_21_GR0_22_061621_1596558875.CSV");
            /* 
           ARGUMENTS:
           DIR:
           CLASSNAME:
           GRMIN:
           GRMAX:
           HRMIN: (OPTIONAL)
           HRMAX: (OPTIONAL)
           MINMAX: (OPTIONAL)
           DAYS: (COMMA SEPARATED)
           HACKT1: (OPTIONAL)
           */
            //String[] szClassroomsToProcess = arguments;// {"DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 LENAVERSION:SP FOLDERSTRUCTURE:DAY GR:0.2,2 DATES:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES"} ;
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" } ;
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019,1/15/2019,2/5/2019,3/6/2019,3/15/2019,5/7/2019,5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/8/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_17// CLASSNAME:LADYBUGS1 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:3/3/2017,3/10/2017,3/17/2017,3/31/2017,4/7/2017,4/21/2017,4/28/2017,5/12/2017,5/19/2017,5/26/2017 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1718// CLASSNAME:LADYBUGS2 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/24/2017,11/3/2017,11/17/2017,12/14/2017,1/11/2018,2/2/2018,2/16/2018,3/13/2018,3/20/2018,5/1/2018,5/16/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/3/2018,10/8/2018,11/28/2018,12/4/2018,1/8/2019,1/15/2019,2/5/2019,3/6/2019,3/15/2019,5/7/2019,5/14/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1819// CLASSNAME:LADYBUGS_1819 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:5/14/2019,5/24/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_1718// CLASSNAME:LADYBUGS2 GRMIN:0.25 GRMAX:1.5 HRMIN:7 HRMAX:13 DAYS:10/24/2017,11/3/2017,11/17/2017,12/14/2017,1/11/2018,2/2/2018,2/16/2018,3/13/2018,3/20/2018,5/1/2018,5/16/2018 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:G://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:12 DAYS:01/23/2019,01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:10 MINMAX:59 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_PM GRMIN:0.2 GRMAX:2 HRMIN:11 HRMAX:1 MINMAX:50 DAYS:01/25/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:C://LVL// CLASSNAME:Starfish_2021 GRMIN:0.2 GRMAX:2 HRMIN:8 HRMAX:13 MINMAX:50 DAYS:11/24/2020" };
            //String[] szClassroomsToProcess = { "DIR:D://CLASSROOMS_OLD// CLASSNAME:APPLETREE_1819 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:16 DAYS:8/30/2018,9/6/2018,9/13/2018,10/4/2018,10/11/2018,11/1/2018,11/8/2018,11/29/2018,12/6/2018,1/10/2019,2/7/2019,2/14/2019,3/7/2019,3/14/2019,4/4/2019,5/9/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            // String[] szClassroomsToProcess = { "DIR:F:// CLASSNAME:StarFish_2021 GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:13 DAYS:3/16/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:F://CLASSROOMS_OLD//PRIDE_LEAP// CLASSNAME:PRIDE_LEAP_AM GRMIN:0.2 GRMAX:2 HRMIN:7 HRMAX:12 DAYS:01/23/2019,02/20/2019,03/20/2019,04/16/2019,5/30/2019 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            String[] szClassroomsToProcess = { "DIR:E:// CLASSNAME:StarFish_2021 JUSTUBICLEANUP:YES GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021,4/13/2021,4/27/2021,5/20/2021,5/25/2021,6/8/2021,6/23/2021,7/28/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };
            //String[] szClassroomsToProcess = { "DIR:E:// CLASSNAME:StarFish_2021 GRMIN:0.2 ANGLE:45 GRMAX:2.5 HRMIN:7 HRMAX:13 DAYS:3/16/2021,4/6/2021 HACKT1:NO LENATIMES:NO ONSETS:YES TEN:YES VEL:NO ANGLES:YES SUMDAY:YES SUMALL:YES ITS:YES GR:YES DBS:YES APPROACH:YES SOCIALONSETS:YES" };


            Boolean ubiCleanup = false;
            /******** A)FOR EACH CLASSROOM:********/
            foreach (String szClassroomArgs in szClassroomsToProcess)
            {
                /*1- Create Classroom Object, read and set Parameters*/
                Classroom classRoom = new Classroom();
                String[] args = szClassroomArgs.Split(' ');
                foreach (String arg in args)
                {
                    String[] setting = arg.Split(':');
                    if (setting.Length > 1)
                    {
                        switch (setting[0].Trim())
                        {
                            case "JUSTUBICLEANUP":
                                ubiCleanup =setting[1].Trim().ToUpper()=="YES";
                                break;
                            case "DIR":
                                classRoom.dir = setting[1].Trim()+ ":"+setting[2].Trim();
                                break;
                            case "ANGLE":
                                classRoom.angle = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "CLASSNAME":
                                classRoom.className = setting[1].Trim();
                                break;
                            case "GRMIN":
                                classRoom.grMin = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "GRMAX":
                                classRoom.grMax = Convert.ToDouble(setting[1].Trim());
                                break;
                            case "HRMIN":
                                classRoom.startHour = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "HRMAX":
                                classRoom.endHour = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "MINMAX":
                                classRoom.endMinute = Convert.ToInt16(setting[1].Trim());
                                break;
                            case "DAYS":
                                foreach(String szDate in setting[1].Trim().Split(','))
                                {
                                    classRoom.classRoomDays.Add(Utilities.getDate(szDate) );
                                }
                                break;
                        }
                    }
                }

                /*2- Set Version Name extension for file naming: GR+minGrwith_insteadOfDots+maxGrwith_insteadOfDots+TodaysMMDDYY+RANDOMNUMBER
                     Set Classroom Object mapId to link mapping files and data
                     Create directories for distinct reports*/
                Utilities.setVersion(classRoom.grMin, classRoom.grMax);//run day and GR version for file naming
                classRoom.mapById = "LONGID";
                classRoom.setDirs();


                /*3- Set Classroom’s Base Mappings */
                classRoom.setBaseMappings();


                /*4 Process Data or clean ubi */
                if(!ubiCleanup)
                    classRoom.process(true);
                else
                    classRoom.clean();



                // classRoom.processGofRfiles();
                // classRoom.processFromGofRfiles("", true);
                //classRoom.processOnsetsGrAndActLogs(); TO DELETE
                classRoom.mergeDayFiles();

                 

                //Utilities.szVersion = "10_26_2020_478216537";// "10_21_2020_2098687227";// "10_20_2020_419130690";// "10_20_2020_986296434";// "10_19_2020_1345568271";//10_19_2020_1345568271  10_19_2020_1700354507
                classRoom.getPairActLeadsFromFiles();





                //classRoom.process();
                //classRoom.mergeSimpleFiles();
                //classRoom.processTenthOfSecs();
            }
             
            Console.ReadLine();
        }
    }
}
