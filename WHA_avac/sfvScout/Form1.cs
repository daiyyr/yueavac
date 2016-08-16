using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace WHA_avac
{
    public partial class Form1 : Form
    {

        /*
         * if the it is true, you can choose working holiday apply or general apply
         * if the it is false, you can only choose working holiday apply 
         */
        bool debug = true;


        /*
         * nothing changed 
         */
        bool FastMode = true;



        public const int retry = 5;

        
        string reg = "";
        Match myMatch;
        int gThreadNo = -1;
        int gThreadNoOfVerificationCodeToBeEntered = -1;

        List<string> gViewstate = new List<string>();
        List<string> gEventvalidation = new List<string>();
        List<string> gVerificationCode = new List<string>();
        List<CookieCollection> gCookieContainer = new List<CookieCollection>();
        List<int> gTicket = new List<int>();
        List<string> urlForStep2 = new List<string>();
        List<string> gHtml = new List<string>();

        string gPassword = "mushroom123";

        


        //  30 for guangzhou;29 for shanghai;28 for beijing
        string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
               gTitle = "MR.",                  //  称呼  MR.  或   MS.
               gContactNumber = "1234567",      // 固定电话
               gEmail = "15985830370@163.com",   //邮箱
               gFirstName = "jinping",          //护照上的名
               gLastName = "xi",                //护照上的姓
               gMobile = "139034000",           //手机
               gPassport = "E722330033",          //护照号
               gSTDCode = "0533",                //区号
               emailPassword = "dyyr7921129",

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;


        /*
        //淘寶訂單號： 956505835532653
        //qq號   379318693
        string gVACity_raw = "chengdu",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "86950176",      // 固定电话
                gEmail = "247418742@qq.com",   //邮箱
                gFirstName = "zhengzheng",          //护照上的名
                gLastName = "Liu",                //护照上的姓
                gMobile = "15823548228",           //手机
                gPassport = "G53479063",          //护照号
                gSTDCode = "023",                //区号
        */

        /*
                //淘寶訂單號：955465021629660
            //qq號:122090260

       string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
               gTitle = "MS.",                  //  称呼  MR.  或   MS.
               gContactNumber = "02150835967",      // 固定电话
               gEmail = "122090260@qq.com",   //邮箱
               gFirstName = "XI",          //护照上的名
               gLastName = "YU",                //护照上的姓
               gMobile = "18521328802",           //手机
               gPassport = "E49972259",          //护照号
               gSTDCode = "021",                //区号
        */

        /*
               //淘寶訂單號 955308927879280
//QQ:452763948
       string gVACity_raw = "chengdu",         // 递签地点  beijing  shanghai  guangzhou  chengdu
               gTitle = "Ms.",                  //  称呼  MR.  或   MS.
               gContactNumber = "68615010",      // 固定电话
               gEmail = "452763948@qq.com",   //邮箱
               gFirstName = "ZIFENG",          //护照上的名
               gLastName = "LI",                //护照上的姓
               gMobile = "13540413772",           //手机
               gPassport = "E20400166 ",          //护照号
               gSTDCode = "028",                //区号
        */

        /*
               //淘寶訂單號：1051264035605579
//qq號 273134980

       string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
               gTitle = "MR.",                  //  称呼  MR.  或   MS.
               gContactNumber = "5702708",      // 固定电话
               gEmail = "273134980@qq.com",   //邮箱
               gFirstName = "rong",          //护照上的名
               gLastName = "zhou",                //护照上的姓
               gMobile = "15889722857",           //手机
               gPassport = "G61601285",          //护照号
               gSTDCode = "0716",                //区号

        */

        /*
        //淘寶訂單號：1063546088353568
        //qq號：2469874685

        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "123456",      // 固定电话
                gEmail = "wstcymhd0@163.com",   //邮箱
                gFirstName = "LING",          //护照上的名
                gLastName = "HUANG",                //护照上的姓
                gMobile = "13761700617",           //手机
                gPassport = "E12833426",          //护照号
                gSTDCode = "021",                //区号


        */
        /*
        //淘寶訂單號：1148125940851538
        //qq號 498816363

        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "88887777",      // 固定电话
                gEmail = "498816363@qq.com",   //邮箱
                gFirstName = "WEI",          //护照上的名
                gLastName = "XU",                //护照上的姓
                gMobile = "13641465710",           //手机
                gPassport = "E11069671",          //护照号
                gSTDCode = "0793",                //区号

        */

        /*
        //淘寶訂單號： 1126995719039860
        //qq號   741326750

        string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "4588956",      // 固定电话
                gEmail = "741326750@qq.com",   //邮箱
                gFirstName = "MIN",          //护照上的名
                gLastName = "ZHOU",                //护照上的姓
                gMobile = "15586278026",           //手机
                gPassport = "E47732503",          //护照号
                gSTDCode = "0728",                //区号


        */

        /*
        //淘寶訂單號：
        //qq號:1210034996

        string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS",                  //  称呼  MR.  或   MS.
                gContactNumber = "5830028",      // 固定电话
                gEmail = "1210034996@qq.com",   //邮箱
                gFirstName = "HAITING",          //护照上的名
                gLastName = "LUO",                //护照上的姓
                gMobile = "13828580195",           //手机
                gPassport = "E01154605",          //护照号
                gSTDCode = "0763",                //区号

        */

        /*
        //淘寶訂單號: 1071798661328027
        //qq號: 425668637
        string gVACity_raw = "shanghai", // 递签地点
        gTitle = "MS.", // 称呼
        gContactNumber = "88231001", // 固定电话 
        gEmail = "376853981@qq.com", //邮箱 
        gFirstName = "WENLIN", //护照上的名 
        gLastName = "DAI", //护照上的姓 
        gMobile = "15088746122", //手机 
        gPassport = "E07173229", //护照号 
        gSTDCode = "0510", //区号


        */
        /*
        //淘寶訂單號：
        //qq號 419233054

        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "56123456",      // 固定电话
                gEmail = "yangshujun1208@163.com",   //邮箱
                gFirstName = "SHUJUN",          //护照上的名
                gLastName = "YANG",                //护照上的姓
                gMobile = "18616148845",           //手机
                gPassport = "E44459231",          //护照号
                gSTDCode = "021",                //区号


        */

        /*
        //淘寶訂單號：954843972968557
        //qq號:77529501

        string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "1234567",      // 固定电话
                gEmail = "77529501@qq.com",   //邮箱
                gFirstName = "LIWEI",          //护照上的名
                gLastName = "ZHANG",                //护照上的姓
                gMobile = "18998332267",           //手机
                gPassport = "E39768052",          //护照号
                gSTDCode = "0533",                //区号

        */

        /*
        //淘寶訂單號：1217744065626679
        //qq號：345710523

        string gVACity_raw = "guangzhou",         // 递签地点 guangzhou  
                gTitle = "MS.",                  //  称呼 
                gContactNumber = "22981991",      // 固定电话
                gEmail = "345710523@qq.com",   //邮箱 
                gFirstName = "jiali",          //护照上的名
                gLastName = "zhang",                //护照上的姓
                gMobile = "15999819140",           //手机
                gPassport = "E47426786",          //护照号
                gSTDCode = "0769",                //区号
        */
        /*
        //淘寶訂單號：
        //qq號 25730015

        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "87863326",      // 固定电话
                gEmail = "25730015@qq.com",   //邮箱
                gFirstName = "JIAN",          //护照上的名
                gLastName = "LIN",                //护照上的姓
                gMobile = "15980612449",           //手机
                gPassport = "E54076262",          //护照号
                gSTDCode = "0591",                //区号
        */
        /*
        //淘寶訂單號：
        //qq號 251620727

        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "84784700",      // 固定电话
                gEmail = "tianyongzhe16@sina.com",   //邮箱
                gFirstName = "CHUNYONG",          //护照上的名
                gLastName = "YIN",                //护照上的姓
                gMobile = "18910580621",           //手机
                gPassport = "G35290903",          //护照号
                gSTDCode = "010",                //区号

        */

        //淘寶訂單號：beyondlolita01
        //qq號 252476094

        /*
        string gVACity_raw = "chengdu",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "7077888",      // 固定电话
                gEmail = "lolita-601@hotmail.com",   //邮箱
                gFirstName = "NINGYI",          //护照上的名
                gLastName = "LIU",                //护照上的姓
                gMobile = "15520537297",           //手机
                gPassport = "G37965867",          //护照号
                gSTDCode = "0816",                //区号

        */

        /*
        //淘寶訂單號：1117344195511596
        //qq號 1435995917

        string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "34876747",      // 固定电话
                gEmail = "1435995917@qq.com",   //邮箱
                gFirstName = "yue",          //护照上的名
                gLastName = "wang",                //护照上的姓
                gMobile = "13826093210",           //手机
                gPassport = "G43554059",          //护照号
                gSTDCode = "020",                //区号

        */

        //淘寶訂單號：1071539876411545
//qq號648949762
        /*
       string gVACity_raw = "shanghai",         // 递签地点   shanghai 
               gTitle = "MR.",                  //  称呼  MR. 
               gContactNumber = "31278780",      // 固定电话 31278780
               gEmail = "648949762@qq.com",   //邮箱648949762qq.com
               gFirstName = "JIAMIN",          //护照上的名JIAMIN
               gLastName = "GU",                //护照上的姓GU
               gMobile = "15150100092",           //手机15150100092
               gPassport = "E16776012",          //护照号E16776012
               gSTDCode = "021",                //区号021

        */


        //淘寶訂單號：913989009445085
        //qq號 324015680
        /*
        string gVACity_raw = "chengdu",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "72732181",      // 固定电话
                gEmail = "790222723@qq.com",   //邮箱
                gFirstName = "KEYU",          //护照上的名
                gLastName = "LI",                //护照上的姓
                gMobile = "18482170359",           //手机
                gPassport = "E57112004",          //护照号
                gSTDCode = "028",                //区号
        */

        /*
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "123456",      // 固定电话
                gEmail = "375079842@qq.com",   //邮箱
                gFirstName = "YICHI",          //护照上的名
                gLastName = "WANG",                //护照上的姓
                gMobile = "13903511728",           //手机
                gPassport = "G54155732",          //护照号
                gSTDCode = "0351",                //区号

        */
        /*
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
               gTitle = "MS.",                  //  称呼  MR.  或   MS.
               gContactNumber = "123456",      // 固定电话
               gEmail = "452763948@qq.com",   //邮箱
               gFirstName = "ZIFENG",          //护照上的名
               gLastName = "LI",                //护照上的姓
               gMobile = "13540413772",           //手机
               gPassport = "E20400166",          //护照号
               gSTDCode = "028",                //区号
        

        //淘寶訂單號：1117344195511596
        //qq號 1435995917

        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "34876747",      // 固定电话
                gEmail = "1435995917@qq.com",   //邮箱
                gFirstName = "yue",          //护照上的名
                gLastName = "wang",                //护照上的姓
                gMobile = "13826093210",           //手机
                gPassport = "G43554059",          //护照号
                gSTDCode = "020",                //区号
        


        //咖啡客户 500元
        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "28888888",      // 固定电话
                gEmail = "auwhver01@163.com",   //邮箱
                gFirstName = "YUN",          //护照上的名
                gLastName = "HUANG",                //护照上的姓
                gMobile = "13585768157",           //手机
                gPassport = "E13230312",          //护照号
                gSTDCode = "021",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码


               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;

        
        
        //小骆  1210034996
        string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "3308556",      // 固定电话
                gEmail = "13828580195@163.com",   //邮箱
                gFirstName = "HAITING",          //护照上的名
                gLastName = "LUO",                //护照上的姓
                gMobile = "13828580195",           //手机
                gPassport = "E01154605",          //护照号
                gSTDCode = "0763",                //区号
                emailPassword = "haiting075020",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
       

        //杨延玲   600代申请预约信
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "3308556",      // 固定电话
                gEmail = "lylalylayang@163.com",   //邮箱
                gFirstName = "YANLING",          //护照上的名
                gLastName = "YANG",                //护照上的姓
                gMobile = "15801529881",           //手机
                gPassport = "E11104519",          //护照号
                gSTDCode = "010",                //区号
                emailPassword = "lyla123456",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
        

        
        //咖啡客户 江笑
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "87692008",      // 固定电话
                gEmail = "auwhv07@163.com",   //邮箱
                gFirstName = "XIAO",          //护照上的名
                gLastName = "JIANG",                //护照上的姓
                gMobile = "13466545457",           //手机
                gPassport = "E03436873",          //护照号
                gSTDCode = "027",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
        
        
        
        //咖啡客户 刘真真
        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "54893255",      // 固定电话
                gEmail = "auwhv01@163.com",   //邮箱
                gFirstName = "ZHENZHEN",          //护照上的名
                gLastName = "LIU",                //护照上的姓
                gMobile = "15821775081",           //手机
                gPassport = "E69265532",          //护照号
                gSTDCode = "021",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;

        //咖啡客户 罗琼
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "87692008",      // 固定电话
                gEmail = "auwhv05@163.com",   //邮箱
                gFirstName = "QIONG",          //护照上的名
                gLastName = "LUO",                //护照上的姓
                gMobile = "15926221656",           //手机
                gPassport = "E49743841",          //护照号
                gSTDCode = "027",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
        

        //上官朋友
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "64313881",      // 固定电话
                gEmail = "15985830370@163.com",   //邮箱
                gFirstName = "TINGTING",          //护照上的名
                gLastName = "SHEN",                //护照上的姓
                gMobile = "18513602595",           //手机
                gPassport = "E02193189",          //护照号
                gSTDCode = "010",                //区号
                emailPassword = "dyyr7921129",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
       
        //咖啡客户 晏阳
        string gVACity_raw = "shanghai",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "87692008",      // 固定电话
                gEmail = "auwhv02@163.com",   //邮箱
                gFirstName = "YANG",          //护照上的名
                gLastName = "YAN",                //护照上的姓
                gMobile = "13419666648",           //手机
                gPassport = "E18480170",          //护照号
                gSTDCode = "027",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
        
        //咖啡客户 李昕妍
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "87692008",      // 固定电话
                gEmail = "auwhv03@163.com",   //邮箱
                gFirstName = "XINYAN",          //护照上的名
                gLastName = "LI",                //护照上的姓
                gMobile = "13502170793",           //手机
                gPassport = "E21369194",          //护照号
                gSTDCode = "022",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
         
        //咖啡客户 李文昭
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "87692008",      // 固定电话
                gEmail = "auwhv04@163.com",   //邮箱
                gFirstName = "WENZHAO",          //护照上的名
                gLastName = "LI",                //护照上的姓
                gMobile = "18502604017",           //手机
                gPassport = "E22812514",          //护照号
                gSTDCode = "022",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
        


        //咖啡客户 刘灿
        string gVACity_raw = "guangzhou",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MR.",                  //  称呼  MR.  或   MS.
                gContactNumber = "87692008",      // 固定电话
                gEmail = "auwhv06@163.com",   //邮箱
                gFirstName = "CAN",          //护照上的名
                gLastName = "LIU",                //护照上的姓
                gMobile = "14715016525",           //手机
                gPassport = "E12711792",          //护照号
                gSTDCode = "0451",                //区号
                emailPassword = "123qwe",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
        
        //tang YANMEI
        string gVACity_raw = "beijing",         // 递签地点  beijing  shanghai  guangzhou  chengdu
                gTitle = "MS.",                  //  称呼  MR.  或   MS.
                gContactNumber = "3308556",      // 固定电话
                gEmail = "15985830370@163.com",   //邮箱
                gFirstName = "YANMEI",          //护照上的名
                gLastName = "TANG",                //护照上的姓
                gMobile = "15545170508",           //手机
                gPassport = "E64144066",          //护照号
                gSTDCode = "028",                //区号
                emailPassword = "dyyr7921129",       //登录邮箱的密码

               gVACity = "",
               gCategory = "from combobox";          //13 for general, 17 for work and holiday;
        */








        List<String> gDays = new List<string>(); //5721 means 2015.08.31, the number of days since 2000.01.01
                                                 //所有线程共同管理一个天数表，只有第一个获得天数页的线程可以添加可用天数
                                                 //默认选择最晚日期

        string gTime = "02";    //02~24, 15分钟一个时段，某个时段满了则后面的时段号均-1
                                //例如
                                /**
                                 *  0730 02
                                    0800 03
                                    0845 04
                                    0915 05
                                    0930 06
                                    0945 07
                                    1030 08
                                    1045 09
                                    1100 10
                                    1115 11
                                    1130 12
                                    1145 13
                                    1200 14
                                    1215 15
                                    1230 16
                                    1245 17
                                    1300 18
                                    1330 19
                                    1345 20
                                    1400 21
                                 **/


 //       Thread gAlarm = null;
 //       string gnrnodeGUID = "";
 //       string gViewStateGenerator = "";
  //      string user = "dudeea";
  //      string password = "Dd123456";


        public Form1()
        {
            InitializeComponent();

            comboBox2.SelectedIndex = 0; //fast
            FastMode = true;

            if (debug)
            {
                this.ClientSize = new System.Drawing.Size(950, 600);
                comboBox1.SelectedIndex = 1; //normal
                button2.Visible = true;
                testLog.Visible = true;
            }
            else
            {
                comboBox1.SelectedIndex = 0; //work and holiday 
                comboBox1.Items.RemoveAt(1);
            }

            gCategory = comboBox1.SelectedIndex == 0 ? "17" : "13";    //13 for general, 17 for work and holiday

            if (gVACity_raw.Equals("guangzhou"))
            {
                gVACity = "30";
            }
            else if (gVACity_raw.Equals("shanghai"))
            {
                gVACity = "29";
            }
            else if (gVACity_raw.Equals("beijing"))
            {
                gVACity = "28";
            }else{
                gVACity = "31";
            }
            gEmail = gEmail.Replace("@", "%40");
            gFirstName = gFirstName.ToUpper();
            gLastName = gLastName.ToUpper();
            if (File.Exists(System.Environment.CurrentDirectory + "\\" + "urlList"))
            {
                string[] lines = File.ReadAllLines(System.Environment.CurrentDirectory + "\\" + "urlList");
                foreach (string line in lines)
                {
                    urlList.Items.Add(line);
                }
            }
        }

        public delegate void setLog(int threadNo, string str1);
        public void setLogT(int threadNo, string s)
        {
            if (logT.InvokeRequired)
            {
                // 实例一个委托，匿名方法，
                setLog sl = new setLog(delegate(int number, string text)
                {
                    logT.AppendText("线程" + number.ToString() + " " + DateTime.Now.ToString() + " " + text + Environment.NewLine);
                });
                // 把调用权交给创建控件的线程，带上参数
                logT.Invoke(sl,threadNo, s);
            }
            else
            {
                logT.AppendText("线程" + threadNo.ToString() + " " + DateTime.Now.ToString() + " " + s + Environment.NewLine);
            }
        }

        public void setLogtRed(int threadNo, string s)
        {
            if (logT.InvokeRequired)
            {
                setLog sl = new setLog(delegate(int number, string text)
                {
                    logT.AppendText("线程" + number.ToString()+" " + DateTime.Now.ToString() + " " + text + Environment.NewLine);
                    int i = logT.Text.LastIndexOf("\n", logT.Text.Length - 2);
                    if (i > 1)
                    {
                        logT.Select(i, logT.Text.Length);
                        logT.SelectionColor = Color.Red;
                        logT.Select(i, logT.Text.Length);
                        logT.SelectionFont = new Font(Font, FontStyle.Bold);
                    }
                });
                logT.Invoke(sl, threadNo, s);
            }
            else
            {
                logT.AppendText("线程" + threadNo.ToString() + " " + DateTime.Now.ToString() + " " + s + Environment.NewLine);
                int i = logT.Text.LastIndexOf("\n", logT.Text.Length - 2);
                if (i > 1)
                {
                    logT.Select(i, logT.Text.Length);
                    logT.SelectionColor = Color.Red;
                    logT.Select(i, logT.Text.Length);
                    logT.SelectionFont = new Font(Font, FontStyle.Bold);
                }
            }
        }

        public delegate void DSetTestLog(HttpWebRequest req, string respHtml);
        public void setTestLog(HttpWebRequest req, string respHtml)
        {
            if (testLog.InvokeRequired)
            {
                DSetTestLog sl = new DSetTestLog(delegate(HttpWebRequest req1, string text)
                {
                    testLog.Text = Environment.NewLine + "返回的HTML源码：";
                    testLog.Text += Environment.NewLine + text;
                });
                testLog.Invoke(sl, req, respHtml);
            }
            else
            {
                testLog.Text = Environment.NewLine + "返回的HTML源码：";
                testLog.Text += Environment.NewLine + respHtml;
            }
        }

      
        /*
        public void alarm()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(WHA_avac.Properties.Resources.mtl);
            player.Load();
            player.PlayLooping();
        }
         */
        public static string ToUrlEncode(string strCode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(strCode); //默认是System.Text.Encoding.Default.GetBytes(str)  
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
            for (int i = 0; i < byStr.Length; i++)
            {
                string strBy = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strBy))
                {
                    //是字母或者数字则不进行转换    
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16));
                }
            }
            return (sb.ToString());
        }

        public void writeFile(string file, string content)
        {
            FileStream aFile = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile);
            sw.Write(content);
            sw.Close();
        }
        
        public int downloadHtml(string html)
        {
            string fileName = System.Environment.CurrentDirectory + "\\" +
                gLastName + gFirstName + System.DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
            string fileFullName = fileName + ".html";
            int i = 2;
            while (true)
            {
                if (File.Exists(fileFullName))
                {
                    fileFullName = fileName + "_" + i.ToString();
                    i++;
                    continue;
                }
                break;
            }
            
            writeFile(fileFullName, html);
            return 1;
        }

        public void setRequest(HttpWebRequest req, int threadNo)
        {
            //req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //req.Accept = "*/*";
            //req.Connection = "keep-alive";
            //req.KeepAlive = true;
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E";
            //req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:37.0) Gecko/20100101 Firefox/37.0";
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E";
            //req.Headers["Accept-Encoding"] = "gzip, deflate";
            //req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            req.Host = "www.visaservices.in";

            req.AllowAutoRedirect = false;
            req.ContentType = "application/x-www-form-urlencoded";
            req.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.10; rv:40.0) Gecko/20100101 Firefox/40.0";
            req.CookieContainer = new CookieContainer();
            req.CookieContainer.PerDomainCapacity = 40;
            if (gCookieContainer[threadNo] != null)
            {
                req.CookieContainer.Add(gCookieContainer[threadNo]);
            }
        }

        public int writePostData(int threadNo, HttpWebRequest req, string data)
        {
            byte[] postBytes = Encoding.UTF8.GetBytes(data);
            req.ContentLength = postBytes.Length;
            Stream postDataStream = null;
            try
            {
                postDataStream = req.GetRequestStream();
                postDataStream.Write(postBytes, 0, postBytes.Length);
                
            }
            catch (WebException webEx)
            {
                setLogT(threadNo,"GetRequestStream," + webEx.Status.ToString());
                return -1;
            }
            
            postDataStream.Close();
            return 1;
        }

        public string resp2html(HttpWebResponse resp)
        {
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(resp.GetResponseStream());
                return stream.ReadToEnd();
            }
            else
            {
                return resp.StatusDescription;
            }
        }

        public static string resp2html(HttpWebResponse resp, string charSet, Form1 form1, int threadNo)
        {
            var buffer = GetBytes(form1, resp, threadNo);
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                if (String.IsNullOrEmpty(charSet) || string.Compare(charSet, "ISO-8859-1") == 0)
                {
                    charSet = GetEncodingFromBody(buffer);
                }

                try
                {
                    var encoding = Encoding.GetEncoding(charSet);  //Shift_JIS
                    var str = encoding.GetString(buffer);

                    return str;
                }
                catch (Exception ex)
                {
                    form1.setLogT(threadNo, "resp2html, " + ex.ToString());
                    return string.Empty;
                }


            }
            else
            {
                return resp.StatusDescription;
            }

        }


        private static byte[] GetBytes(Form1 form1, WebResponse response, int threadNo)
        {
            var length = (int)response.ContentLength;
            byte[] data;

            using (var memoryStream = new MemoryStream())
            {
                var buffer = new byte[0x100];
                try
                {
                    using (var rs = response.GetResponseStream())
                    {
                        for (var i = rs.Read(buffer, 0, buffer.Length); i > 0; i = rs.Read(buffer, 0, buffer.Length))
                        {
                            memoryStream.Write(buffer, 0, i);
                        }
                    }
                }
                catch (Exception e)
                {
                    form1.setLogT(threadNo, "read ResponseStream: " + e.ToString()); //500
                }


                data = memoryStream.ToArray();
            }

            return data;
        }

        private static string GetEncodingFromBody(byte[] buffer)
        {
            var regex = new Regex(@"<meta(\s+)http-equiv(\s*)=(\s*""?\s*)content-type(\s*""?\s+)content(\s*)=(\s*)""text/html;(\s+)charset(\s*)=(\s*)(?<charset>[a-zA-Z0-9-]+?)""(\s*)(/?)>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            var str = Encoding.ASCII.GetString(buffer);
            var regMatch = regex.Match(str);
            if (regMatch.Success)
            {
                var charSet = regMatch.Groups["charset"].Value;
                return charSet;
            }

            return Encoding.ASCII.BodyName;
        }


        /* 
         * return success or not
         */
        public int weLoveMuYue(int threadNo, string url, string method, string referer, bool allowAutoRedirect, string postData)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req, threadNo);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
            
                if (method.Equals("POST"))
                {
                    if (writePostData(threadNo, req, postData) < 0)
                    {
                        continue;
                    }
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    setLogT(threadNo, "respStreamReader, " + webEx.Status.ToString());
                    continue;
                }
                if (resp != null)
                {
                    respHtml = resp2html(resp);
                    if (respHtml.Equals(""))
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
                setTestLog(req, respHtml);
                gCookieContainer[threadNo] = req.CookieContainer.GetCookies(req.RequestUri);
                resp.Close();
                break;
            }
            return 1;
        }

        /* 
         * return response HTML
         */
        public string weLoveYue(int threadNo, string url, string method, string referer, bool allowAutoRedirect, string postData)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req, threadNo);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
            
                if (method.Equals("POST"))
                {
                    if (writePostData(threadNo, req, postData) < 0)
                    {
                        continue;
                    } 
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    setLogT(threadNo, "respStreamReader, " + webEx.Status.ToString());
                    continue;
                }
                if (resp != null)
                {
                    respHtml = resp2html(resp);
                    if (respHtml.Equals(""))
                    {
                        continue;
                    }
                    gCookieContainer[threadNo] = req.CookieContainer.GetCookies(req.RequestUri);
                    if (debug)
                    {
                        setTestLog(req, respHtml);
                    }
                    resp.Close();
                    return respHtml;
                }
                else
                {
                    continue;
                }
            }
        }

        /* 
         * return responsive HTML
         * unregular host
         */
        public string weLoveYue(int threadNo, Form1 form1, string url, string method, string referer, bool allowAutoRedirect, string postData, string host, bool responseInUTF8)
        {
            for (int i = 0; i < retry; i++)
            {
                /*
                if (gForceToStop)
                {
                    break;
                }
                */ 
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req, threadNo);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
                req.Host = host;
                if (method.Equals("POST"))
                {
                    if (writePostData(threadNo, req, postData) < 0)
                    {
                        continue;
                    }
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    form1.setLogT(threadNo, "GetResponse, " + webEx.Status.ToString());
                    if (webEx.Status == WebExceptionStatus.ConnectionClosed)
                    {
                        return "wrong address"; //地址错误
                    }
                    if (webEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        form1.setLogT(threadNo, "本次请求被服务器拒绝，可尝试调高间隔时间"); //500
                    }
                    continue;
                }
                if (resp != null)
                {
                    if (responseInUTF8)
                    {
                        respHtml = resp2html(resp);
                    }
                    else
                    {
                        respHtml = resp2html(resp, resp.CharacterSet, form1, threadNo); // like  Shift_JIS
                    }
                    if (respHtml.Equals(""))
                    {
                        continue;
                    }
                    gCookieContainer[threadNo] = req.CookieContainer.GetCookies(req.RequestUri);
                    if (debug)
                    {
                        form1.setTestLog(req, respHtml);
                    }
                    resp.Close();
                    return respHtml;
                }
                else
                {
                    continue;
                }
            }
            return "";
        }


        /*
         * do not handle the response
         */
        public HttpWebResponse weLoveYueer(int threadNo, HttpWebResponse resp, string url, string method, string referer, bool allowAutoRedirect, string postData)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                setRequest(req, threadNo);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
            
                if (method.Equals("POST"))
                {
                    if (writePostData(threadNo, req, postData) < 0)
                    {
                        continue;
                    } 
                }
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    setLogT(threadNo, "respStreamReader, " + webEx.Status.ToString());
                    continue;
                }
                if (resp != null)
                {
                    gCookieContainer[threadNo] = req.CookieContainer.GetCookies(req.RequestUri);
                    return resp;
                }
                else
                {
                    continue;
                }
            }
        }



        public int apply1(int threadNo) 
        {

            
            setLogT(threadNo, "step1");
            setLogT(threadNo, "get first page..");

            string respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                "GET",
                "",
                true,
                "");

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

            }

            

            setLogT(threadNo, "make a pointment..");
            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                false,
                "__EVENTTARGET=ctl00%24plhMain%24lnkSchApp&__EVENTARGUMENT=&__VIEWSTATE=" + gViewstate[threadNo]
                + "&____Ticket=" + gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                );
            gTicket[threadNo]++;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode( myMatch.Groups[0].Value);
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode( myMatch.Groups[0].Value);

            }

            
            setLogT(threadNo, "selecting Location: " + gVACity_raw);

        verification1:
            ThreadStart starter = delegate { showVerificationCode(respHtml, threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }

            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppScheduling.aspx",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                false,
                "__VIEWSTATE="+gViewstate[threadNo]
                + "&ctl00%24plhMain%24cboVAC=" + gVACity
                + "&ctl00%24plhMain%24btnSubmit=%E6%8F%90%E4%BA%A4&ctl00%24plhMain%24hdnValidation1"
                + "=%E8%AF%B7%E9%80%89%E6%8B%A9%EF%BC%9A&ctl00%24plhMain%24hdnValidation2=%E7%AD%BE%E8%AF%81%E7%94%B3%E8"
                + "%AF%B7%E4%B8%AD%E5%BF%83&ctl00%24plhMain%24hdnValidation3=%E5%B1%85%E4%BD%8F%E5%9B%BD&____Ticket="+gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode( myMatch.Groups[0].Value);
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode( myMatch.Groups[0].Value);
            }
            if (respHtml.Contains("Please enter the correct verification code"))
            {
                setLogT(threadNo, "验证码错误！请重新输入");
                goto verification1;
            }
            

            /*
            
            setLogT("select the visa type..");
            //neccessary?
            respHtml = weLoveYue(
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingGetInfo.aspx",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingGetInfo.aspx",
                false,
                "__EVENTTARGET=ctl00%24plhMain%24cboVisaCategory&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE=" + gViewstate
                + "&ctl00%24plhMain%24tbxNumOfApplicants=1&ctl00%24plhMain%24cboVisaCategory=" + gCategory //13 for general, 17 for working and holiday
                + "&ctl00%24plhMain%24hdnValidation1=%E8%AF%B7%E8%BE%93%E5%85%A5%EF%BC%9A&ctl00%24plhMain%24hdnValidation2=%E6%9C%89%E6%95%88%E4%BA%BA%E6"
                +"%95%B0%E3%80%82&ctl00%24plhMain%24hdnValidation3=%E6%8A%A5%E5%90%8D%E4%BA%BA%E6%95%B0%E5%BF%85%E9%A1"
                +"%BB%E4%BB%8B%E4%BA%8E1%E5%92%8C++&ctl00%24plhMain%24hdnValidation4=%E7%AD%BE%E8%AF%81%E7%B1%BB%E5%88%AB"
                + "&____Ticket="+gTicket.ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation
                );
            gTicket++;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation = myMatch.Groups[0].Value;

            }
            gEventvalidation = ToUrlEncode(gEventvalidation);

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate = myMatch.Groups[0].Value;

            }
            gViewstate = ToUrlEncode(gViewstate);

            */

            delegate2 s0 = new delegate2(delegate()
            {
                setLogT(threadNo, "submitting the visa type: " + comboBox1.SelectedItem.ToString());
            });
            textBox1.Invoke(s0);

            /**** 选择签证类别步骤不需要验证码
             * 
             * 
        verification2:
            starter = delegate { showVerificationCode(respHtml, threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }
            */

            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingGetInfo.aspx",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingGetInfo.aspx",
                false,
                "__EVENTTARGET=&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE=" + gViewstate[threadNo]
                +"&ctl00%24plhMain%24tbxNumOfApplicants"
                +"=1&ctl00%24plhMain%24cboVisaCategory=" + gCategory //13 for general, 17 for working and holiday
                +"&ctl00%24plhMain%24btnSubmit=%E6%8F%90%E4%BA%A4&ctl00%24plhMain"
                +"%24hdnValidation1=%E8%AF%B7%E8%BE%93%E5%85%A5%EF%BC%9A&ctl00%24plhMain%24hdnValidation2=%E6%9C%89%E6"
                +"%95%88%E4%BA%BA%E6%95%B0%E3%80%82&ctl00%24plhMain%24hdnValidation3=%E6%8A%A5%E5%90%8D%E4%BA%BA%E6%95"
                +"%B0%E5%BF%85%E9%A1%BB%E4%BB%8B%E4%BA%8E1%E5%92%8C++&ctl00%24plhMain%24hdnValidation4=%E7%AD%BE%E8%AF"
                +"%81%E7%B1%BB%E5%88%AB&____Ticket="+gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空

            if (respHtml.Contains("No date(s) available for appointment.") || respHtml.Contains("无日期（S）委任"))
            {
                setLogT(threadNo, "该类别名额已满，不要灰心，备战下一次");
                return -9;
            }

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode( myMatch.Groups[0].Value);
                
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode( myMatch.Groups[0].Value);
                
            }
            /*
            if (respHtml.Contains("Please enter the correct verification code"))
            {
                setLogT(threadNo, "验证码错误！请重新输入");
                goto verification2;
            }
            */
        verification3:
            starter = delegate { showVerificationCode(respHtml, threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }

            setLogT(threadNo, "filling in email address..");

            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/EmailRegistration.aspx",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingGetInfo.aspx",
                false,
                "__VIEWSTATE=" + gViewstate[threadNo]
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                + "&ctl00%24plhMain%24txtCnfPassword="
                + gPassword
                + "&ctl00%24plhMain%24txtEmailID="
                + gEmail
                + "&ctl00%24plhMain%24txtPassword="
                + gPassword
                + "&ctl00%24plhMain%24ImageButton1=%E6%8F%90%E4%BA%A4"
                + "&____Ticket=" + gTicket[threadNo].ToString()
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空

            if (respHtml.Contains("Please enter the correct verification code"))
            {
                setLogT(threadNo, "验证码错误！请重新输入");

                //因为这是step1的最后一步,所以只有在验证码输错的情况下才需要去获取Viewstate和Eventvalidation
                reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
                }

                reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

                }

                goto verification3;
            }
            else
            {
                setLogT(threadNo, "邮件已送出, step1 finish");
            }

            Mail163 myMail = new Mail163(threadNo, gEmail, emailPassword, this);
            
            //urlForStep2[threadNo] = myMail.queery("预约注册网址", @"https://www\.visaservices(\s|\S)+?(?=</a>)");
            urlForStep2[threadNo] = myMail.queery("Appointment Registration URL", @"https://www\.visaservices(\s|\S)+?(?=</a>)");
            return 1;
        }



        public int apply2(int threadNo) {
            setLogT(threadNo, "step2");
            gTicket[threadNo] = 1;

            string p = "";
            reg = @"(?<=aspx\?P=)(\s|\S)+?(?=$)";
            myMatch = (new Regex(reg)).Match(urlForStep2[threadNo]);
            if (myMatch.Success)
            {
                p = ToUrlEncode(myMatch.Groups[0].Value);
            }
            

            string respHtml = weLoveYue(
                threadNo,
                urlForStep2[threadNo],
                "GET",
                "",
                true,
                "");

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

            }

            setLogT(threadNo, "filling in details..");

        verification1:
            ThreadStart starter = delegate { showVerificationCode(respHtml, threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }
            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingVisaCategory.aspx?p="+p,
                "POST",
                urlForStep2[threadNo],
                false,
                "__VIEWSTATE=" + gViewstate[threadNo]
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxPassportNo="
                + gPassport
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24cboTitle="
                + gTitle
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxFName="
                + gFirstName
                + "&ctl00%24plhMain"
                + "%24repAppVisaDetails%24ctl01%24tbxLName="
                + gLastName
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxSTDCode="
                + gSTDCode
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxContactNumber="
                + gContactNumber
                + "&ctl00%24plhMain%24repAppVisaDetails"
                + "%24ctl01%24tbxMobileNumber="
                + gMobile
                + "&ctl00%24plhMain%24btnSubmit=Submit"
                + "&ctl00%24plhMain%24hdnValidation1=Please+enter+Passport+No.+of+Applicant+no.&ctl00%24plhMain%24hdnValidation2"
                + "=Please+select+title+for+applicant+no.&ctl00%24plhMain%24hdnValidation3=Please+enter+given+name%28s%29"
                + "+of+Applicant+no.&ctl00%24plhMain%24hdnValidation4=Please+enter+surname+of+Applicant+no.&ctl00%24plhMain"
                + "%24hdnValidation5=Please+enter+mobile+no.+for+applicant+no.&ctl00%24plhMain%24hdnValidation6=Please+enter"
                + "+valid+Area+Code+for+applicant+no.&ctl00%24plhMain%24hdnValidation7=Please+enter+valid+email+address"
                + "+for+applicant+no.&ctl00%24plhMain%24hdnValidation8=Please+enter+valid+GWFNo+for+applicant+no.&ctl00"
                + "%24plhMain%24hdnValidation9=Please+select+Visa+Class+for+applicant+no."
                + "&____Ticket=" + gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空
            gHtml[threadNo] = respHtml;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

            }

            if (respHtml.Contains("Please enter the correct verification code") || respHtml.Contains("Please enter the correct CAPTCHA alphabets"))
            {
                setLogT(threadNo, "验证码错误！请重新输入");
                goto verification1;
            }


            if (respHtml.Contains("申请人已预约") || respHtml.Contains("Marked applicants already have an appointment."))
            {
                setLogtRed(threadNo, "申请人已预约: " + gLastName + " " + gFirstName + ", 护照: " + gPassport);
                return -3;
            }

            if (respHtml.Contains("Applicant Details") && respHtml.Contains("Please enter"))
            {
                setLogT(threadNo, "incompleted personal details");
                return -1;
            }
            if (gDays.Count == 0)
            {
                reg = @"(?<=Appointment',')\d+?(?='\)"" style=""color:Black"" title="")";
                myMatch = (new Regex(reg)).Match(respHtml);
                while (myMatch.Success)
                {
                    gDays.Add(myMatch.Groups[0].Value);
                    myMatch = myMatch.NextMatch();
                }
            }

            if (gDays.Count == 0)
            {
                setLogT(threadNo, gVACity_raw + ", " + (gCategory.Equals("17") ? "working and holiday, " : "general") + ", 名额已满, 请尝试其它预约地点");
                return -1;
            }

            



            //we do not need to calculate the TimeSpan, it's on the page
            /*
            string dateDay="";
            string dateMonth="";
            int dateYear = 2015;
            
            //find the month
            reg = @"(?<=style=""color:Black"" title="").*?(?= \d+"")";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                dateMonth = myMatch.Groups[0].Value;
            }
            else
            {
                setLogT("Error.. there is no available month");
                return -1;
            }

            //find a latest day
            for (int i = 31; i > 0; i--)
            {
                reg = i.ToString()+@""">"+i.ToString()+@"</a></td><td align";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    dateDay = i.ToString();
                    break;
                }
            }
            if (dateDay.Equals(""))
            {
                setLogT(gVACity_raw + ", " + (gCategory.Equals("17")?"working and holiday, ":"general") + ", 名额已满!");
                return -1;
            }

            //find the year
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyyMMMMd";
            while (true)
            {
                DateTime dt = Convert.ToDateTime(dateYear.ToString() + dateMonth + dateDay, dtFormat);
                if ((dt - DateTime.Now).Days > 0)
                {
                    gDays[1] = (dt - Convert.ToDateTime("2000-01-01 00:00:00")).Days.ToString();
                    break;
                }
                dateYear++;
            }
             */
            return 1;
        }



        public int pickDate(int threadNo)
        {

        verification1:
            ThreadStart starter = delegate { showVerificationCode(gHtml[threadNo], threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }

            string respHtml;
            while(true)
            {
                setLogT(threadNo, "pickDate..");
                lock (gDays)
                {
                    if (gDays.Count == 0)
                    {
                        setLogT(threadNo, gVACity_raw + ", " + (gCategory.Equals("17") ? "working and holiday, " : "general") + ", 名额已满, 请尝试其它预约地点");
                        return -1;
                    }
                    respHtml = weLoveYue(
                        threadNo,
                        "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingInterviewDate.aspx",
                        "POST",
                        "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingVisaCategory.aspx",
                        false,
                        "__EVENTTARGET=ctl00%24plhMain%24cldAppointment&__EVENTARGUMENT="
                        + gDays.Last()
                        + "&__VIEWSTATE=" + gViewstate[threadNo]
                        + "&____Ticket=" + gTicket[threadNo].ToString()
                        + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                        + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                        );
                    gTicket[threadNo]++;
                    gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空
                    gHtml[threadNo] = respHtml;
                }

                reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

                }

                reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

                }

                if (respHtml.Contains("Please enter the correct verification code"))
                {
                    setLogT(threadNo, "验证码错误！请重新输入");
                    goto verification1;
                }
                
                reg = @"style=""color:Black"" title="".*?"">\d+</a></td><td align=""center""";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    lock (gDays)
                    {
                        if (gDays.Count > 0)
                        {
                            setLogT(threadNo, "所选日期已被占满，自动选择前一个可用天");
                            gDays.RemoveAt(gDays.Count - 1);
                            continue;
                        }
                        else
                        {
                            setLogT(threadNo, gVACity_raw + ", " + (gCategory.Equals("17") ? "working and holiday, " : "general") + ", 名额已满, 请尝试其它预约地点");
                            return -1;
                        }
                        
                    }
                }
                return 1;
            }
        }

        public int pickTime(int threadNo)
        {
            setLogT(threadNo, "pickTime..");

        verification1:
            ThreadStart starter = delegate { showVerificationCode(gHtml[threadNo], threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }

            string respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingInterviewDate.aspx",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppSchedulingInterviewDate.aspx",
                false,
                "__EVENTTARGET=ctl00%24plhMain%24gvSlot%24ctl" + gTime
                + "%24lnkTimeSlot&__EVENTARGUMENT="
                + "&__VIEWSTATE=" + gViewstate[threadNo]
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                + "&____Ticket="+gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION="+ gEventvalidation[threadNo]
            );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空
            gHtml[threadNo] = respHtml;

            if (respHtml.Contains("Your appointment is Rescheduled")) //新邮箱的成功提示信息?
            {
                printAppLetter(threadNo);
            }
            else
            {
                reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
                }

                reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

                }


                if (respHtml.Contains("Please enter the correct verification code"))
                {
                    setLogT(threadNo, "验证码错误！请重新输入");
                    goto verification1;
                }

                lock (gDays)
                {
                    if (gDays.Count > 0)
                    {
                        setLogT(threadNo, "所选时间异常, 重新提交前一个可用日期");
                        gDays.RemoveAt(gDays.Count - 1);
                        pickDate(threadNo);
                        pickTime(threadNo);
                    }
                    else
                    {
                        setLogT(threadNo, gVACity_raw + ", " + (gCategory.Equals("17") ? "working and holiday, " : "general") + ", 名额已满, 请尝试其它预约地点");
                        return -1;
                    }
                }
            }

            return 1;
        }

        public void autoT(int threadNo)
        {

            if (apply1(threadNo) == -9)
            {
                return;
            }

            apply2(threadNo);

            if (pickDate(threadNo) == -1)
            {
                return;
            }

            pickTime(threadNo);
        }

        public void autoT2(int threadNo)
        {
            gThreadNoOfVerificationCodeToBeEntered = -1;

            int fillInResult = apply2(threadNo);

            if (fillInResult == -2)//incorrect vervification code
            {
                return;
            }
            if (fillInResult == -1 || fillInResult == -3)
            {
                return;
            }

            if (pickDate(threadNo) == -1)
            {
                return;
            }

            pickTime(threadNo);
            
        }


        public void printAppLetter(int threadNo)
        {
            setLogT(threadNo, "start to print AppLetter... ");

            gViewstate[threadNo] = "zN6xXaY%2FnQNmaaIlERdi7LQl%2BBtTJSWlQckAPk" +
                                    "%2B4oQpDovIGW80RqFi8gdy3WhVH9%2FaN7mJd%2BMEmlZBEsSF%2ByOrvGBQmXgcDAi%2BO9AZeeh%2FvK93W1m3x4J2IF47SmIiHIhH2iS" +
                                    "%2For3foC1jhAbq3mE2y7gVlT2PW0PVHQcOWIyTnacwRm1yz7MUOv0C4D6ErgIGBblYp1Eq%2FkCbk1RwOkYRsHTE9jCaRPaEdsfmgDXqVo2Jj44CXh7DJpwpTz" +
                                    "%2B9Kce5uTWQgsAeK63DU2oIDGuqRS%2BDFuwERMTl0bhGpkJQ6lURgByidtd%2FpdAi5OaiK2%2BYBbueGbIYCnxcBiQqswxO4IUTWj9dFUHiiVkSlbPdZ6Fqc4JsiEP6WTb2zKy7BtsceJJmN59AQAGFBNLYQSAD1A8k" +
                                    "%2BDekyhJ5Vp65n8SHJKcu3gTh32VGAWhiailxZioWVkiJZZsWb6tp6M1Uo%2FFdZj8Ol8Y2gRFt2hjRJzs%2FhD0gzkllIOqPWIgoD9vn9" +
                                    "%2B2qUiBdHIWE%3D";
            gEventvalidation[threadNo] = "MfAzSYnx%2FBo9NlbEJZGqAfsO1qbH2Pbq2qGK8OTeqfnJLJ52qCApEepqQ%2BUvWVZdGuavmxNvymnyQeocxo4k3Q%3D%3D";


            string respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d",
                false,
                "__EVENTTARGET=ctl00%24plhMain%24lnkPrintApp"
                + "&__EVENTARGUMENT="
                + "&__VIEWSTATE=" + gViewstate[threadNo]
                + "&____Ticket="+gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION="+ gEventvalidation[threadNo]
            );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空
            gHtml[threadNo] = respHtml;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

            }
            else
            {
                goto exception;
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);

            }
            else
            {
                goto exception;
            }

            setLogT(threadNo, "passport and sur name verification... ");
        verification1:
            ThreadStart starter = delegate { showVerificationCode(respHtml, threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }

            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/EmailRegistration.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d",
                false,
                "__VIEWSTATE=" + gViewstate[threadNo]
                + "&ctl00%24plhMain%24ImageButton1=Submit&____Ticket=" + gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                + "&ctl00%24plhMain%24txtEmailID=" + gEmail
                + "&ctl00%24plhMain%24txtPassword=" + gPassword
                );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
            }
            else
            {
                goto exception;
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
            }
            else
            {
                goto exception;
            }
            if (respHtml.Contains("Please enter the correct verification code"))
            {
                setLogT(threadNo, "验证码错误！请重新输入");
                goto verification1;
            }

            string referenceNo = "";
            reg = @"(?<=ctl00_plhMain_rdbDocketList_0"">)(\s|\S)*?(?=<\/label>)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                referenceNo = ToUrlEncode(myMatch.Groups[0].Value);
            }
            else
            {
                goto exception;//此处加入no reference no的判断
            }

            setLogT(threadNo, "Select Reference Number... ");
        verification2:
            starter = delegate { showVerificationCode(respHtml, threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }

            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/EmailRegistration.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/EmailRegistration.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d",
                false,
                "__VIEWSTATE=" + gViewstate[threadNo]
                + "&ctl00%24plhMain%24btnSubmit=Submit&____Ticket=" + gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                + "&ctl00%24plhMain%24rdbDocketList=" + referenceNo
                );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
            }
            else
            {
                goto exception;
            }

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate[threadNo] = ToUrlEncode(myMatch.Groups[0].Value);
            }
            else {
                goto exception;
            }
            if (respHtml.Contains("Please enter the correct verification code"))
            {
                setLogT(threadNo, "验证码错误！请重新输入");
                goto verification2;
            }

            setLogT(threadNo, "passport and sur name verification...");
        verification3:
            starter = delegate { showVerificationCode(respHtml, threadNo); };
            new Thread(starter).Start();

            while (gVerificationCode[threadNo] == null || gVerificationCode[threadNo] == "")
            {
                Thread.Sleep(50);
            }

            respHtml = weLoveYue(
                threadNo,
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/PrintAppLetter.aspx",
                "POST",
                "https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/EmailRegistration.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d",
                false,
                "__VIEWSTATE=" + gViewstate[threadNo]
                + "&ctl00%24plhMain%24btnSubmit=Submit&____Ticket=" + gTicket[threadNo].ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation[threadNo]
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode[threadNo]
                + "&ctl00%24plhMain%24tbxPassport=" + gPassport
                + "&ctl00%24plhMain%24tbxLastName=" + gLastName.ToUpper()
                );
            gTicket[threadNo]++;
            gVerificationCode[threadNo] = "";//不论输入得正确与否, 都需要清空

            if (respHtml.Contains("Please enter the correct verification code"))
            {
                setLogT(threadNo, "验证码错误！请重新输入");
                goto verification3;
            }

            string lblReference = "";
            reg = @"(?<=<span id=""lblReference"">).*?(?=</span>)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                lblReference = myMatch.Groups[0].Value;
                setLogT(threadNo, "预约号: " + lblReference + "\n确认信已下载到软件所在位置，请截图或拍照并请牢记预约号。");
            
                string result = respHtml
                .Replace("<head id=\"Head1\"><title>", "<head id=\"Head1\"><meta charset=\"UTF-8\"><title>")
                .Replace("../", "https://www.visaservices.in/DIAC-China-Appointment_new/")
                .Replace("Print Appointment Letter", "打印预约信")
                .Replace("AppWelcome.aspx\">Home</a>", "AppWelcome.aspx\">主页</a>")
                .Replace("Australian Visa Application Centre", "澳大利亚签证申请中心")
                .Replace("Guangzhou", "广州")
                .Replace("Shanghai", "上海")
                .Replace("Beijing", "北京")
                .Replace("Confirmation of Appointment", "预约确认")
                .Replace("Please arrive at the Visa Application Centre not earlier than 10 minutes before the given time.<br>", "请在预约时间前10分钟抵达签证申请中心。")//英文网页多一个br
                .Replace("Please remember to carry the following with you :", "请携带：")
                .Replace("1. Your passport", "1. 护照")
                .Replace("2. A completed and signed visa form", "2. 填写并签署的申请表")
                .Replace("3. Printout of the checklist for the visa type being applied for", "3. 打印所申请签证类别适用的审核清单")
                .Replace("4. Supporting documentation listed in the checklist", "4. 审核清单中所列的支持性文件")
                .Replace("5. The fee amount – please check our website for the visa fee details and mode of payment.", "5. 费用-请在我们的网站上查询签证费详情和付款方式。")
                .Replace("Entry into the Visa Application Centre is regulated by a security check. Please carry a print out of this appointment confirmation letter so that the same can be verified from our Daily Appointment List at the information counter.",
                "进入签证申请中心需接受安检。请携带此预约信以便认证您的预约信息。")
                .Replace("Reference Number", "确认号码")
                .Replace("Visa Category", "签证类别")
                .Replace("Appointment Date", "预约日期")
                .Replace("Appointment Time", "预约时间")
                .Replace("Sr. No.", "序列号码")
                .Replace("Full Name", "姓名")
                .Replace("Passport Number", "护照号码")
                .Replace("Address of VAC", "签证申请中心地址")
                .Replace("Unit 02, 29/F, HM Tower, No. 3, Jinsui Road,", "广州市天河区珠江新城金穗路3号")
                .Replace("Zhujiang New Town, Tianhe District, 广州  ", "汇美大厦29楼02单元 邮编：")
                .Replace("Enquiry Number", "问询号码")
                .Replace("2nd Floor, Jiushi Commercial Building,", "中国上海市黄浦区四川中路213号久事商务大厦2层,")
                .Replace("No. 213 Middle Sichuan Rd., Huangpu District, 上海, 200002, China ", "邮编200002. ")
                .Replace("Room C,D,E,F,G,H,I, 21st Floor", "中国北京东城区东直门外大街")
                .Replace("Oriental Kenzo Plaza, No.48 Dongzhimenwai Street Dongcheng District, 北京 , P.R. China 100027", "48号东方银座写字楼21层D-I室 邮编：100027 ");
                
                downloadHtml(result);
                return;
            }


            exception:
                setLogtRed(threadNo, "网站异常, 打印预约信失败, 请稍后再试! 或者自行登录AVAC官网打印预约信");
                setLogtRed(threadNo, "打印方式:");
                setLogtRed(threadNo, "1. 访问这个地址 https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/AppWelcome.aspx?p=sPcgcjykQzBJn3ZQhoWvHUCcn911JlTQwOXWcGhM4%2fE%3d  点击\"打印预约信\" ");
                setLogtRed(threadNo, "2. 在电子邮件处输入" + gEmail.Replace("%40", "@") + ", 在密码处输入" + gPassword);
                setLogtRed(threadNo, "3. 在下一个页面中, 填写您的护照号(passport number)和姓氏拼音(sur name), 确认号码不需要填写");
        }


        private void loginB_Click(object sender, EventArgs e)
        {
//            Thread t = new Thread(loginT);
//            t.Start();
        }

        private void autoB_Click(object sender, EventArgs e)
        {
            lock (autoB)
            {
                gThreadNo++;
                gViewstate.Add("");
                gEventvalidation.Add("");
                gVerificationCode.Add("");
                gCookieContainer.Add(null);
                gTicket.Add(1);
                urlForStep2.Add("");
                gHtml.Add("");

                ThreadStart starter = delegate { autoT(gThreadNo); };
                new Thread(starter).Start();

                //使用线程池
                //WaitCallback callback = delegate(object state) { autoT((int)state); };
                //ThreadPool.QueueUserWorkItem(callback, gThreadNo);

                //使用ParameterizedThreadStart
                //Thread tt = new Thread(new ParameterizedThreadStart(autoT));
                //t.Start(gThreadNo);
            }
        }

        private void rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void logT_TextChanged(object sender, EventArgs e)
        {
            logT.SelectionStart = logT.Text.Length;
            logT.ScrollToCaret();
        }

        public delegate void delegate2();


        /*
        public void addURL()
        {
            //string P = @"^http(s)?:\/\/([\w-]+\.)+[\w-]+$";//无法匹配下级页面
            string P = @"^(https?|ftp|file)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]";
            Match M = (new Regex(P)).Match(inputT.Text);
            if (M.Success)
            {
            }else{
                MessageBox.Show("invalid url!");
                return;
            }            
            if (urlList.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    urlList.Items.Add(inputT.Text);
                    inputT.Text = "";
                });
                urlList.Invoke(sl);
            }
            else
            {
                urlList.Items.Add(inputT.Text);
                inputT.Text = "";
            }
            string strCollected = string.Empty;
            for (int i = 0; i < urlList.Items.Count; i++)
            {
                if (strCollected == string.Empty)
                {
                    strCollected = urlList.GetItemText(urlList.Items[i]);
                }
                else
                {
                    strCollected += "\n" + urlList.GetItemText(urlList.Items[i]) ;
                }
            }
            writeFile(System.Environment.CurrentDirectory + "\\" + "urlList", strCollected);
        }

       
        public void deleteURL()
        {
            if (urlList.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    for (int i = urlList.CheckedItems.Count - 1; i >= 0; i--)
                    {
                        urlList.Items.Remove(urlList.CheckedItems[i]);
                    }
                });
                urlList.Invoke(sl);
            }
            else
            {
                for (int i = urlList.CheckedItems.Count - 1; i >= 0; i--)
                {
                    urlList.Items.Remove(urlList.CheckedItems[i]);
                }
            }
            string strCollected = string.Empty;
            for (int i = 0; i < urlList.Items.Count; i++)
            {
                if (strCollected == string.Empty)
                {
                    strCollected = urlList.GetItemText(urlList.Items[i]);
                }
                else
                {
                    strCollected += "\n" + urlList.GetItemText(urlList.Items[i]);
                }
            }
            writeFile(System.Environment.CurrentDirectory + "\\" + "urlList", strCollected);
        }
        */
        private void addB_Click(object sender, EventArgs e)
        {
       //     Thread t = new Thread(addURL);
       //     t.Start();
        }

        private void deleteB_Click(object sender, EventArgs e)
        {
       //     Thread t = new Thread(deleteURL);
       //     t.Start();
        }

        public void showVerificationCode(string respHtml, int threadNo)
        {
            string cCodeGuid = "";
            reg = @"(?<=MyCaptchaImage.aspx\?guid=).*?(?="" border=)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                cCodeGuid = myMatch.Groups[0].Value;
            }
            lock (pictureBox1)
            {
                while (gThreadNoOfVerificationCodeToBeEntered != -1)
                {
                    Thread.Sleep(50);
                }
                gThreadNoOfVerificationCodeToBeEntered = threadNo;
                if (textBox1.InvokeRequired)
                {
                    delegate2 sl = new delegate2(delegate()
                    {
                        pictureBox1.ImageLocation = @"https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/MyCaptchaImage.aspx?guid=" + cCodeGuid;
                        pictureBox1.Refresh();
                        pictureBox1.Visible = true;
                        textBox1.Text = "";
                        textBox1.ReadOnly = false;
                        textBox1.Focus();
                        label6.Text = "线程" + threadNo.ToString() + ":请输入验证码";
                        label6.Visible = true;
                    });
                    textBox1.Invoke(sl);
                }
                else
                {
                    pictureBox1.ImageLocation = @"https://www.visaservices.in/DIAC-China-Appointment_new/AppScheduling/MyCaptchaImage.aspx?guid=" + cCodeGuid;
                    pictureBox1.Refresh();
                    pictureBox1.Visible = true;
                    textBox1.Text = "";
                    textBox1.ReadOnly = false;
                    textBox1.Focus();
                    label6.Text = "线程" + threadNo.ToString() + ":请输入验证码";
                    label6.Visible = true;
                }
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 5)
            {
                gVerificationCode[gThreadNoOfVerificationCodeToBeEntered] = textBox1.Text.Substring(0, 5);
                if (textBox1.InvokeRequired)
                {
                    delegate2 sl = new delegate2(delegate()
                    {
                        textBox1.ReadOnly = true;
                        pictureBox1.Visible = false;
                        pictureBox1.ImageLocation = "";
                    });
                    textBox1.Invoke(sl);
                }
                else
                {
                    textBox1.ReadOnly = true;
                    pictureBox1.Visible = false;
                    pictureBox1.ImageLocation = "";
                }
                gThreadNoOfVerificationCodeToBeEntered = -1;


        //        ThreadStart starter = delegate { autoT2(gThreadNoOfVerificationCodeToBeEntered); };
        //        new Thread(starter).Start();

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    textBox1.Text = "";
                    textBox1.ReadOnly = false;
                    textBox1.Focus();
                    label6.Visible = true;
                });
                textBox1.Invoke(sl);
            }
            else
            {
                textBox1.Text = "";
                textBox1.ReadOnly = false;
                textBox1.Focus();
                label6.Visible = true;
            }
        }

        string testWetherThreadUseUnicValue = "CCCCCCC";
        public void tA()
        {
            testWetherThreadUseUnicValue = "AAAAAAAAA";
            for (int t = 0; t < 50; t++)
            {
                setLogT(-1, "tA: " + testWetherThreadUseUnicValue);
            }
        }
        public void tB()
        {
            
            for (int t = 0; t < 50; t++)
            {
                setLogT(-1, "tB: " + testWetherThreadUseUnicValue);
            }
            testWetherThreadUseUnicValue = "BBBBBBBBBBB";
        }
        //for test
        private void button2_Click(object sender, EventArgs e)
        {

            //testWetherThreadUseUnicValue
            ThreadStart starter = delegate { tB(); };
            new Thread(starter).Start();
            starter = delegate { tA(); };
            new Thread(starter).Start();
            //答: 不同线程不会使用独立的类成员

            //test TimeSpan
            DateTime dt1 = Convert.ToDateTime("1996-01-01 00:00:00");
            DateTime dt20000101 = Convert.ToDateTime("1997-01-01 00:00:00");
            setLogT(-1,(dt1-dt20000101).Days.ToString());
            int dd = (dt1 - dt20000101).Days;

            //正则表达式有另一条规则，比懒惰／贪婪规则的优先级更高：最先开始的匹配拥有最高的优先权
            reg = @"(?<=a).*?(?=b)";
            myMatch = (new Regex(reg)).Match("aaaaaaaaaaa333b");
            if (myMatch.Success)
            {
                setLogT( -1, myMatch.Groups[0].Value);
            }

            //test string 2 DateTime
            int year = 2015;
            string strDt = year.ToString() + "April25";
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyyMMMMd";
            DateTime dt = Convert.ToDateTime(strDt, dtFormat);
            setLogT(-1,dt.ToString());

            reg = @"(?<=style=""color:Black"" title="").*?(?= \d+"")";
            myMatch = (new Regex(reg)).Match("ent','5750')\" style=\"color:Black\" title=\"September 29\">29</a></td><td align=\"center\" ");
            if (myMatch.Success)
            {
                setLogT( -1,myMatch.Groups[0].Value);
            }
            setLogT(-1,comboBox1.SelectedItem.ToString());

            gDays.Add("1 data");
            gDays.Add("2 data");
            gDays.Add("3 data");
            setLogT(-1,gDays.Last());
            setLogT(-1,gDays[0]);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gCategory = comboBox1.SelectedIndex == 0 ? "17" : "13";    //13 for general, 17 for work and holiday
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FastMode = comboBox1.SelectedIndex == 0 ? true : false;    //13 for general, 17 for work and holiday
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gThreadNo++;//为下载预约新开一个线程
            gViewstate.Add("");
            gEventvalidation.Add("");
            gVerificationCode.Add("");
            gCookieContainer.Add(null);
            gTicket.Add(1);
            urlForStep2.Add("");
            gHtml.Add("");

            ThreadStart starter = delegate { printAppLetter(gThreadNo); };
            new Thread(starter).Start();
        }
    }
}

//选择类别时，是否不需要提交两次: 是，不需要.
//直接post a inavailable date, could succeed?  不可以
//不需要访问第一页？  如果不get首页，最终将返回英文预约页，且英文预约者在中文页重新申请，仍显示名额满；所以可跳过首页的GET和POST , 替换预约页
//多线程处理多次点击

//自动识别验证码
//如果出现两个月的日期，能不翻页直接提交第二个月的最晚时间？
