using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailFingerPrinter
{
    public partial class FingerPrinterForm : Form
    {
        static string MailsPath = "";
        static string TrainSetPath = "";
        static string FingerPrintFilePath = "";
        static string FingerPrintTo = "";
        static List<FingerPrintResult> FingerPrintResultList = new List<FingerPrintResult>();
        public FingerPrinterForm()
        {
            InitializeComponent();
        }
        #region tab_1 给tab_1的所有按钮委托写上签名
        private void bt_mailsfrom_Click(object sender, EventArgs e)
        {
            if (WhereMailIn.ShowDialog() == DialogResult.OK) 
            {
                MailsPath = WhereMailIn.SelectedPath;
                tb_mailfrom.Text = MailsPath;
            }
        }
        private void bt_tomfg_Click(object sender, EventArgs e)
        {
            if (WhereToCreateMFG.ShowDialog() == DialogResult.OK)
            {
                FingerPrintTo = WhereToCreateMFG.SelectedPath;
                tb_mfgto.Text = FingerPrintTo;
            }
        }
        private void bt_create_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MailsPath) || string.IsNullOrEmpty(FingerPrintTo))
            {
                MessageBox.Show("Please input the directory");
            }
            else
            {
                if (!Directory.Exists(MailsPath))
                {
                    MessageBox.Show($"The following directory does not exist, please select once again：\r\n{ MailsPath }");
                }
                else if (!Directory.Exists(FingerPrintTo))
                {
                    MessageBox.Show($"The following directory does not exist, please select once again：\r\n{ FingerPrintTo }");
                }
                else
                {
                    FingerPrint fingerprint = FingerPrint.FromSourceFile(MailsPath, cb_hashead.Checked);
                    fingerprint.SaveTo(FingerPrintTo);
                    MessageBox.Show("Generate successfully！");
                }
            }
        }
        #endregion
        #region tab_2 给tab_2的所有按钮委托写上签名
        private void bt_testfingerprinterfrom_Click(object sender, EventArgs e)
        {
            if (WhichMFG.ShowDialog() == DialogResult.OK)
            {
                string ismfg = WhichMFG.FileName.Substring(WhichMFG.FileName.Length - 4);
                if (ismfg != ".mfg") 
                {
                    MessageBox.Show("please select a fingerprint file（.mfg）");
                }
                else
                {
                    FingerPrintFilePath = WhichMFG.FileName;
                    tb_mfgfrom.Text = FingerPrintFilePath;
                }
            }
        }
        private void bt_trainfrom_Click(object sender, EventArgs e)
        {
            if (WhereTrainMFGIn.ShowDialog() == DialogResult.OK)
            {
                var mfgs = Directory.GetFiles(WhereTrainMFGIn.SelectedPath, "*.mfg");
                if (mfgs.Length == 0)
                {
                    MessageBox.Show("There is no fingerprint file（.mfg） in this directory, please select again");
                }
                else
                {
                    TrainSetPath = WhereTrainMFGIn.SelectedPath;
                    tb_trainfrom.Text = TrainSetPath;
                }
            }
        }
        private void bt_getresult_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TrainSetPath) || string.IsNullOrEmpty(FingerPrintFilePath))
            {
                MessageBox.Show("Please input the directory and file");
            }
            else
            {
                if (!Directory.Exists(TrainSetPath))
                {
                    MessageBox.Show($"The following directory does not exist, please select once again：\r\n{ TrainSetPath }");
                }
                else if (!File.Exists(FingerPrintFilePath))
                {
                    MessageBox.Show($"The following file does not exist, please select once again：\r\n{ FingerPrintFilePath }");
                }
                else
                {
                    string thisMailsPath = MailsPath;
                    string thisTrainSetPath = TrainSetPath;
                    string thisFingerPrintFilePath = FingerPrintFilePath;
                    string thisFingerPrintTo = FingerPrintTo;
                    //还原初始状态
                    Clear();
                    string[] trains = Directory.GetFiles(thisTrainSetPath, "*.mfg");
                    var trainfp = new List<FingerPrint>();
                    FingerPrint testfp = FingerPrint.FromSourceMFGFile(thisFingerPrintFilePath);
                    foreach (var train in trains)
                    {
                        trainfp.Add(FingerPrint.FromSourceMFGFile(train));
                    }
                    foreach (var fp in trainfp)
                    {
                        //将数据写入DataGridView中
                        var resultnow = new FingerPrintResult(testfp, fp);
                        FingerPrintResultList.Add(resultnow);
                        int indexnow = dGV_resulttable.Rows.Add();
                        dGV_resulttable.Rows[indexnow].Cells[0].Value = resultnow.VarianceResult;
                        dGV_resulttable.Rows[indexnow].Cells[1].Value = resultnow.HighRateWordsResult;
                        dGV_resulttable.Rows[indexnow].Cells[2].Value = resultnow.LongRichnessResult;
                        dGV_resulttable.Rows[indexnow].Cells[3].Value = resultnow.WordRichnessResult;
                        dGV_resulttable.Rows[indexnow].Cells[4].Value = resultnow.AdjRateResult;
                        dGV_resulttable.Rows[indexnow].Cells[5].Value = resultnow.FirstLineCodeResult;
                        cb_results.Items.Add(resultnow);
                    }
                    MessageBox.Show("completed. please check the results！");
                }
            }
        }
        #endregion
        /// <summary>
        /// 任何时候执行这个方法，即还原程序到初始状态
        /// </summary>
        void Clear()
        {
            cb_results.Items.Clear();
            DataTable dt = (DataTable)dGV_resulttable.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                dGV_resulttable.DataSource = dt;
            }
            MailsPath = "";
            TrainSetPath = "";
            FingerPrintFilePath = "";
            FingerPrintTo = "";
            FingerPrintResultList = new List<FingerPrintResult>();
            tb_mailfrom.Text = "";
            tb_mfgfrom.Text = "";
            tb_mfgto.Text = "";
            tb_trainfrom.Text = "";
        }
        /// <summary>
        /// 画五边形的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_painter_Click(object sender, EventArgs e)
        {
            int index = cb_results.SelectedIndex;
            if (index < 0)
            {
                MessageBox.Show("Select a result in the combobox!");
                return;
            }
            FingerPrintResult nowresult = FingerPrintResultList[index];
            FingerPrint a = nowresult.a;
            FingerPrint b = nowresult.b;
            Bitmap resultimg = new Bitmap(pB_resultimgbox.Width, pB_resultimgbox.Height);
            Graphics g = Graphics.FromImage(resultimg);
            g.Clear(Color.White);
            Brush b_blue = new SolidBrush(Color.Blue);
            Pen p_black = new Pen(Brushes.Black);
            Pen p_blue = new Pen(Brushes.Blue);
            Pen p_red = new Pen(Brushes.Red, 3);
            Pen p_green = new Pen(Brushes.Green, 3);
            Pen p_dot_blue = new Pen(Brushes.Blue);
            Pen p_whitesmoke = new Pen(Brushes.Gray);
            p_dot_blue.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            #region 计算得到五边形的中心与五角
            PointF center = new PointF(175, 175);
            PointF pointA = new PointF(175, 75);
            PointF pointB = new PointF(274, 161);
            PointF pointC = new PointF(233.8f, 256);
            PointF pointD = new PointF(116.2f, 256);
            PointF pointE = new PointF(76, 161);
            #endregion
            #region 画五边形
            //     g.FillEllipse(b_blue, 175, 175, 5, 5);
            DrawPentagon(g, p_whitesmoke, pointA, pointB, pointC, pointD, pointE);
            g.DrawLine(p_dot_blue, center, pointA);
            g.DrawLine(p_dot_blue, center, pointB);
            g.DrawLine(p_dot_blue, center, pointC);
            g.DrawLine(p_dot_blue, center, pointD);
            g.DrawLine(p_dot_blue, center, pointE);
            #endregion
            #region 画特征点所属的五边形
            PointF aA = GetPosition(center, pointA, 0.5);
            PointF bA = GetPosition(center, pointA, nowresult.HowManyEqualInTop30 * 0.5 / 30); 
            PointF aB = GetPosition(center, pointB, 0.5);
            PointF bB = GetPosition(center, pointB, a.LongRichness / b.LongRichness * 0.5);
            PointF aC = GetPosition(center, pointC, 0.5);
            PointF bC = GetPosition(center, pointC, a.WordRichness / b.WordRichness * 0.5);
            PointF aD = GetPosition(center, pointD, 0.5);
            PointF bD = GetPosition(center, pointD, a.AdjRate / b.AdjRate * 0.5);
            PointF aE = GetPosition(center, pointE, 0.5);
            PointF bE = GetPosition(center, pointE, (10 - FirstLineCode.Judge(a.FirstLineCode, b.FirstLineCode)) * 0.05);
            DrawPentagon(g, p_green, aA, aB, aC, aD, aE);
            DrawPentagon(g, p_red, bA, bB, bC, bD, bE);
            #endregion
            //将picturebox的image属性指向内存中的bitmap
            pB_resultimgbox.Image = resultimg;
        }
        /// <summary>
        /// 用给定的五点按顺时针顺序画五边形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p">画笔</param>
        /// <param name="pointA">五边形顶角</param>
        /// <param name="pointB">五边形右上角</param>
        /// <param name="pointC">五边形右下角</param>
        /// <param name="pointD">五边形左下角</param>
        /// <param name="pointE">五边形左上角</param>
        private void DrawPentagon(Graphics g, Pen p, PointF pointA, PointF pointB, PointF pointC, PointF pointD, PointF pointE)
        {
            g.DrawLine(p, pointA, pointB);
            g.DrawLine(p, pointB, pointC);
            g.DrawLine(p, pointC, pointD);
            g.DrawLine(p, pointD, pointE);
            g.DrawLine(p, pointE, pointA);
        }
        /// <summary>
        /// 用线性插值法得到value值对应的点的具体位置
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private PointF GetPosition(PointF begin, PointF end, double value)
        {
            float x = (end.X - begin.X) * (float)value + begin.X;
            float y = (end.Y - begin.Y) * (float)value + begin.Y;
            return new PointF(x, y);
        }
        
    }
    /// <summary>
    /// 封装了进行io文件操作的方法
    /// </summary>
    public static class IOMethod
    {
        /// <summary>
        /// 获得给定目录中所有邮件的正文（不含引用与回复文本）
        /// </summary>
        /// <param name="path">给定目录</param>
        /// <param name="hashead">邮件是否含头信息</param>
        /// <returns></returns>
        public static string GetBodies(string path, bool hashead = true)
        {
            string result = "";
            var filenames = Directory.GetFiles(path);
            foreach (var file in filenames)
            {
                result += GetBody(file, false);
                result += " ";
            }
            return result;
        }
        /// <summary>
        /// 获得给定文件中的邮件正文（不含引用与回复文本）
        /// </summary>
        /// <param name="path">给定文件路径</param>
        /// <param name="hashead">邮件是否含头信息</param>
        /// <returns></returns>
        public static string GetBody(string path, bool hashead = true)
        {
            string all = "";
            using (StreamReader sr = new StreamReader(path))
            {
                string now = "";
                int rowlimit = 2;
                int emptyrow = 0;
                if (hashead)
                {
                    Regex headall = new Regex("X-FileName:");
                    while ((now = sr.ReadLine()) != null)
                    {
                        if (headall.IsMatch(now))
                        {
                            break;
                        }
                    }
                }
                Regex tailall2 = new Regex("--- Forwarded");
                Regex tailall = new Regex("-Original Message-");
                while ((now = sr.ReadLine()) != null)
                {
                    if (Regex.IsMatch(now, "^\\s+$") || now == "")
                    {
                        emptyrow++;
                        if (emptyrow == rowlimit)
                        {
                            break;
                        }
                    }
                    else
                    {
                        emptyrow = 0;
                    }
                    if (tailall.IsMatch(now) || tailall2.IsMatch(now))
                    {
                        break;
                    }
                    all += now + "\r\n";
                }
            }
            return all;
        }
        /// <summary>
        /// 获得文件中的单词总数，并直接重命名文件，将其写在文件名前
        /// </summary>
        /// <param name="file">文件路径</param>
        public static void ShowTheWordsNumInFile(string file)
        {
            string body = GetBody(file);
            string[] words = GetWords(body);
            int totalword = words.Length;

            string newfilename = GetPathToFile(file) + "/(" + totalword + ")" + GetFileName(file);
            File.Move(file, newfilename);
        }
        /// <summary>
        /// 获取文件或目录最后一个'\\'后的字符（一般为文件名或目录名）
        /// </summary>
        /// <param name="filepath">文件或目录路径</param>
        /// <returns></returns>
        public static string GetFileName(string filepath)
        {
            var namearray = filepath.Split('\\');
            return namearray[namearray.Length - 1];
        }
        /// <summary>
        /// 获取一个文件所属目录的路径
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns></returns>
        public static string GetPathToFile(string file)
        {
            FileInfo fi = new FileInfo(file);
            return fi.Directory.ToString();
        }
        /// <summary>
        /// 获取邮件正文中所有的单词，以字符串数组形式输出
        /// </summary>
        /// <param name="body">邮件正文</param>
        /// <returns></returns>
        public static string[] GetWords(string body)
        {
            Regex allregex = new Regex("[0-9\"-\\?,\r\n:=\\.]+");
            body = allregex.Replace(body, " ");
            Regex kongbai = new Regex("\\s+");
            body = kongbai.Replace(body, " ");
            string[] allarray = body.Split(' ');
            return allarray;
        }
        /// <summary>
        /// 获取邮件正文中所有的单词，以List泛型形式输出，泛型值为EWord，具体说明见EWord类
        /// </summary>
        /// <param name="body">邮件正文</param>
        /// <returns></returns>
        public static List<EWord> GetWordsList(string body)
        {
            #region 统计重复单词数前先进行一个排序的预处理，从而减小复杂度
            string[] words = GetWords(body);
            Array.Sort(words);
            int len = words.Length;
            int first = 0; int second = 0;
            List<EWord> resultlist = new List<EWord>();
            for (; second < len; second++)
            {
                if (words[first] != words[second])
                {
                    resultlist.Add(new EWord(second - first, words[first]));
                    first = second;
                }
            }
            #endregion
            //排序，传入的是一个兰姆达表达式，表示按EWord中times属性的倒序排列
            resultlist.Sort((a, b) =>
            {
                return -1 * a.times.CompareTo(b.times);
            });
            return resultlist;
        }
        /// <summary>
        /// 获得目录中的文件总数，并直接重命名目录，将其写在目录名前
        /// </summary>
        /// <param name="path"></param>
        public static void ShowTheFilesNumInPath(string path)
        {
            var directory = Directory.GetDirectories(path);
            foreach (var item in directory)
            {
                var files = Directory.GetFiles(item, "*.txt");
                int total = files.Length;
                string result = "";
                for (int i = item.Length - 1; i >= 0; i--)
                {
                    if (item[i] == '\\')
                    {
                        result += item.Substring(0, i + 1);
                        result += total;
                        result += item.Substring(i + 1);
                        break;
                    }
                }
                Directory.Move(item, result);
            }
        }
        /// <summary>
        /// 将一个目录内的文件全部移到另一个目录中
        /// </summary>
        /// <param name="apath">源目录</param>
        /// <param name="bpath">终点目录</param>
        public static void MoveAllFileFromAtoB(string apath, string bpath)
        {
            var backfilespath = Directory.GetFiles(apath);
            foreach (var item in backfilespath)
            {
                File.Move(item, bpath + "\\" + GetFileName(item));
            }
        }
        /// <summary>
        /// 以流的形式将文本写入文件中，并释放流对象
        /// </summary>
        /// <param name="FilePath">目标路径</param>
        /// <param name="Content">待写文本</param>
        public static void SaveLog(string FilePath, string Content)
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.Write(Content);
                sw.Flush();
            }
        }
        /// <summary>
        /// 运用完全洗牌算法将源目录内，特定数量的文件移到终点目录内
        /// </summary>
        /// <param name="MotherSetPath">源目录</param>
        /// <param name="TestSetPath">终点目录（通常是测试目录）</param>
        /// <param name="TestSetSize">移动数量（通常是测试集样本容量）</param>
        public static void MoveMotherSetToTestSet(string MotherSetPath, string TestSetPath, int TestSetSize)
        {
            //完全洗牌算法处理allfilesname数组，从而随机的抽出一定数量的样本
            Random m = new Random();
            var allfilesname = Directory.GetFiles(MotherSetPath);
            int minlimit = Math.Max(allfilesname.Length - 1 - TestSetSize, -1);
            for (int i = allfilesname.Length - 1; i > minlimit; i--)
            {
                int nowrandom = m.Next(0, i + 1);
                string temp = allfilesname[i];
                allfilesname[i] = allfilesname[nowrandom];
                allfilesname[nowrandom] = temp;
                File.Move(allfilesname[i], TestSetPath + "\\" + GetFileName(allfilesname[i]));
            }
        }
    }
    /// <summary>
    /// 封装测试时使用的代码，部分代码用于获取数据以供在matlab中进行拟合，正式项目中不使用
    /// </summary>
    public static class Lazy_MailHandle
    {
        #region getpoint for matlab painting
        public static string lazy_GetManyPointAboutWordAndAdj()
        {
            string body = IOMethod.GetBodies(@"C:\Users\Administrator\Desktop\five\spilt_five\chris.dorland@enron.com", false);
            var wordarray = IOMethod.GetWords(body);
            List<Point> result = new List<Point>();
            int adj = 0;
            for (int i = 0; i < wordarray.Length; i++)
            {
                if (AdjJudge.IsAdj(wordarray[i]))
                {
                    adj++;
                }
                if (i % 330 == 0)
                    result.Add(new Point(i, adj));
                Console.WriteLine(i);
            }
            string x = "x=["; string y = "y=[";
            foreach (var item in result)
            {
                x += item.x + ",";
                y += item.y + ",";
            }
            x += "];\r\n";
            y += "];";
            return x + y;
        }
        public static string lazy_GetManyPointAboutWordAndKinds()
        {
            string body = IOMethod.GetBodies(@"C:\Users\Administrator\Desktop\five\spilt_five\carol.clair@enron.comtestmails", false);
            var wordarray = IOMethod.GetWords(body);
            List<Point> result = new List<Point>();
            for (int i = 0; i < wordarray.Length; i += 330)
            {
                string[] fronti = new string[i];
                for (int k = 0; k < i; k++)
                {
                    fronti[k] = wordarray[k];
                }
                Array.Sort(fronti);
                int len = fronti.Length;
                int first = 0; int second = 0;
                List<EWord> resultlist = new List<EWord>();
                for (; second < len; second++)
                {
                    if (fronti[first] != fronti[second])
                    {
                        resultlist.Add(new EWord(second - first, fronti[first]));
                        first = second;
                    }
                }
                result.Add(new Point(i, resultlist.Count));
                Console.WriteLine(i);
            }
            string x = "x=["; string y = "y=[";
            foreach (var item in result)
            {
                x += item.x + ",";
                y += item.y + ",";
            }
            x += "];\r\n";
            y += "];";
            return x + y;
        }
        #endregion
        #region some method for test
        //高频词算法
        public static void lazy_CompareMostWords()
        {
            Console.WriteLine("tm要多少份测试？");
            int nums = Convert.ToInt32(Console.ReadLine());

            string MotherPath = @"C:\Users\Administrator\Desktop\five\spilt_five\chris.dorland@enron.com";
            string aimPath = @"C:\Users\Administrator\Desktop\five\spilt_five\testmails";
            IOMethod.MoveMotherSetToTestSet(MotherPath, aimPath, nums);
            #region 
            string allTrainText = IOMethod.GetBodies(MotherPath, false);
            var TrainTextList = IOMethod.GetWordsList(allTrainText);

            string TrainOutput = "";
            int TrainTotalLitter = 0;
            for (int i = 0; i < TrainTextList.Count; i++)
            {
                TrainTotalLitter += TrainTextList[i].times;
            }
            for (int i = 0; i < 30 && i < TrainTextList.Count; i++)
            {
                TrainOutput += TrainTextList[i].ToString(TrainTotalLitter);
            }

            string TestSetPath = @"C:\Users\Administrator\Desktop\five\spilt_five\testmails";
            string allTestText = IOMethod.GetBodies(TestSetPath, false);
            var TestTextList = IOMethod.GetWordsList(allTestText);
            string TestOutput = "";
            int TestTotalLitter = 0;
            for (int i = 0; i < TestTextList.Count; i++)
            {
                TestTotalLitter += TestTextList[i].times;
            }
            int howmanyhas = 0;
            for (int i = 0; i < 30 && i < TestTextList.Count; i++)
            {
                if (TestTextList[i].isExistIn(TrainTextList))
                {
                    howmanyhas++;
                    TestOutput += "存在";
                }
                TestOutput += TestTextList[i].ToString(TestTotalLitter);
            }
            TestOutput += $"测试集前30条一共有{ howmanyhas }条在训练集前30条中出现";

            //写output
            IOMethod.SaveLog($@"C:\Users\Administrator\Desktop\gaoping\test{ TestTotalLitter }.txt", TestOutput);
            IOMethod.SaveLog($@"C:\Users\Administrator\Desktop\gaoping\train{ TrainTotalLitter }.txt", TrainOutput);
            Console.WriteLine($"测试集前30条一共有{ howmanyhas }条在训练集前30条中出现");
            #endregion
            //换回去
            IOMethod.MoveAllFileFromAtoB(TestSetPath, MotherPath);
        }

        public static void lazy_CompareKindOfWord()
        {
            Console.WriteLine("tm要多少份测试？");
            int nums = Convert.ToInt32(Console.ReadLine());

            string MotherPath = @"C:\Users\Administrator\Desktop\five\spilt_five\chris.dorland@enron.com";
            string aimPath = @"C:\Users\Administrator\Desktop\five\spilt_five\testmails";
            IOMethod.MoveMotherSetToTestSet(MotherPath, aimPath, nums);
            #region 
            string allTrainText = IOMethod.GetBodies(MotherPath, false);
            var TrainTextList = IOMethod.GetWordsList(allTrainText);

            string TrainOutput = "";
            int TrainTotalWords = 0;
            for (int i = 0; i < TrainTextList.Count; i++)
            {
                TrainTotalWords += TrainTextList[i].times;
                TrainOutput += TrainTextList[i];
            }

            string TestSetPath = @"C:\Users\Administrator\Desktop\five\spilt_five\testmails";
            string allTestText = IOMethod.GetBodies(TestSetPath, false);
            var TestTextList = IOMethod.GetWordsList(allTestText);
            string TestOutput = "";
            int TestTotalWords = 0;
            for (int i = 0; i < TestTextList.Count; i++)
            {
                TestTotalWords += TestTextList[i].times;
                TestOutput += TestTextList[i];
            }


            //写output
            IOMethod.SaveLog($@"C:\Users\Administrator\Desktop\gaoping\test{ TestTotalWords }.txt", TestOutput);
            IOMethod.SaveLog($@"C:\Users\Administrator\Desktop\gaoping\train{ TrainTotalWords }.txt", TrainOutput);
            Console.WriteLine($"训练集有词:{TrainTotalWords},词种类:{TrainTextList.Count},种类比总词:{((double)TrainTextList.Count) / (Math.Sqrt((double)TrainTotalWords))}");
            Console.WriteLine($"测试集有词:{TestTotalWords},词种类:{TestTextList.Count},种类比总词:{((double)TestTextList.Count) / (Math.Sqrt((double)TestTotalWords))}");
            #endregion
            //换回去
            IOMethod.MoveAllFileFromAtoB(TestSetPath, MotherPath);
        }
        public static void lazy_CompareAdj()
        {
            Console.WriteLine("tm要多少份测试？");
            int nums = Convert.ToInt32(Console.ReadLine());

            string MotherPath = @"C:\Users\Administrator\Desktop\five\spilt_five\carol.clair@enron.com";
            string aimPath = @"C:\Users\Administrator\Desktop\five\spilt_five\testmails";
            IOMethod.MoveMotherSetToTestSet(MotherPath, aimPath, nums);
            MotherPath = @"C:\Users\Administrator\Desktop\five\spilt_five\chris.dorland@enron.com";
            #region 
            string allTrainText = IOMethod.GetBodies(MotherPath, false);
            var TrainTextList = IOMethod.GetWordsList(allTrainText);

            string TrainOutput = "";
            int TrainTotalWords = 0;
            int TrainAdjWords = 0;
            for (int i = 0; i < TrainTextList.Count; i++)
            {
                TrainTotalWords += TrainTextList[i].times;
                if (TrainTextList[i].isAdj)
                {
                    TrainAdjWords += TrainTextList[i].times;
                }
                TrainOutput += TrainTextList[i];
            }

            string TestSetPath = @"C:\Users\Administrator\Desktop\five\spilt_five\testmails";
            string allTestText = IOMethod.GetBodies(TestSetPath, false);
            var TestTextList = IOMethod.GetWordsList(allTestText);
            string TestOutput = "";
            int TestTotalWords = 0;
            int TestAdjWords = 0;
            for (int i = 0; i < TestTextList.Count; i++)
            {
                TestTotalWords += TestTextList[i].times;
                if (TestTextList[i].isAdj)
                {
                    TestAdjWords += TestTextList[i].times;
                }
                TestOutput += TestTextList[i];
            }

            //写output
            IOMethod.SaveLog($@"C:\Users\Administrator\Desktop\gaoping\test{ TestTotalWords }.txt", TestOutput);
            IOMethod.SaveLog($@"C:\Users\Administrator\Desktop\gaoping\train{ TrainTotalWords }.txt", TrainOutput);
            Console.WriteLine($"训练集有词:{TrainTotalWords},其中形容词:{TrainAdjWords},形容词比总词:{((double)TrainAdjWords) / ((double)TrainTotalWords)}");
            Console.WriteLine($"测试集有词:{TestTotalWords},其中形容词:{TestAdjWords},形容词比总词:{((double)TestAdjWords) / ((double)TestTotalWords)}");
            #endregion
            //换回去
            MotherPath = @"C:\Users\Administrator\Desktop\five\spilt_five\carol.clair@enron.com";
            IOMethod.MoveAllFileFromAtoB(TestSetPath, MotherPath);
        }
        //句号逗号算法
        public static void lazy_CompareJuDot()
        {
            Console.WriteLine("tm要多少份测试？");
            int nums = Convert.ToInt32(Console.ReadLine());

            string MotherPath = @"C:\Users\Administrator\Desktop\five\spilt_five\d..steffes@enron.com";
            string aimPath = @"C:\Users\Administrator\Desktop\five\spilt_five\testmails";
            IOMethod.MoveMotherSetToTestSet(MotherPath, aimPath, nums);
            string allTrainText = IOMethod.GetBodies(MotherPath, false);
            int dot = 0; int ju = 0;
            for (int i = 0; i < allTrainText.Length; i++)
            {
                if (allTrainText[i] == ',')
                    dot++;
                else if (allTrainText[i] == '.')
                    ju++;
            }
            Console.WriteLine("训练集中：");
            Console.WriteLine("dot:" + dot);
            Console.WriteLine("ju:" + ju);
            Console.WriteLine("dot/ju:" + ((double)dot) / ((double)ju));
            string allTestText = IOMethod.GetBodies(aimPath, false);
            dot = 0; ju = 0;
            for (int i = 0; i < allTestText.Length; i++)
            {
                if (allTestText[i] == ',')
                    dot++;
                else if (allTestText[i] == '.')
                    ju++;
            }
            Console.WriteLine("测试集中：");
            Console.WriteLine("dot:" + dot);
            Console.WriteLine("ju:" + ju);
            Console.WriteLine("dot/ju:" + ((double)dot) / ((double)ju));
            //换回去
            IOMethod.MoveAllFileFromAtoB(aimPath, MotherPath);
        }
        #endregion
    }
    /// <summary>
    /// 针对给定的邮件集合，能够生成特定的指纹类的实例，
    /// 同时可以用指纹类的实例比较多个邮件集合之间的相似程度，具体见FingerPrintResult类
    /// </summary>
    public class FingerPrint
    {
        /// <summary>
        /// 指纹文件中：
        /// 第一行：称谓书写习惯状态码
        /// 第二行：形容词使用频率
        /// 第三行：用词丰富性
        /// 第四行：长短句使用线段
        /// 后全部：高频词
        /// </summary>
        public string SourceMailName = null;
        public FirstLineCode FirstLineCode { get; }
        public double AdjRate { get; }
        public double WordRichness { get; }
        public double LongRichness { get; }
        public List<EWord> HighRateWords { get; }
        /// <summary>
        /// 私有方法，只能类内部调用，保证指纹类型的安全
        /// </summary>
        /// <param name="firstlinecode"></param>
        /// <param name="adjrate"></param>
        /// <param name="wordrichness"></param>
        /// <param name="longrichness"></param>
        /// <param name="highratewords"></param>
        private FingerPrint(FirstLineCode firstlinecode, double adjrate, double wordrichness, double longrichness, List<EWord> highratewords)
        {
            FirstLineCode = firstlinecode;
            AdjRate = adjrate;
            WordRichness = wordrichness;
            LongRichness = longrichness;
            HighRateWords = highratewords;
        }
        /// <summary>
        /// 以流的形式将实例存储到文件中，其中高频词只保留前30个
        /// </summary>
        /// <param name="savepath">目标路径</param>
        public void SaveTo(string savepath)
        {
            if (savepath[savepath.Length - 1] != '\\')
            {
                savepath += '\\';
            }
            string allwordsrichness = "";
            for (int i = 0; i < 30 && i < HighRateWords.Count; i++)
            {
                allwordsrichness += HighRateWords[i].ToString();
            }
            string output = FirstLineCode + "\r\n"
                + AdjRate + "\r\n"
                + WordRichness + "\r\n"
                + LongRichness + "\r\n"
                + allwordsrichness;
            string filename = IOMethod.GetFileName(SourceMailName);
            if (string.IsNullOrEmpty(filename))
            {
                filename = "unnamed.mfg";
            }
            else
            {
                filename += ".mfg";
            }
            IOMethod.SaveLog(savepath + filename, output);
        }
        /// <summary>
        /// 从邮件集合中实例化指纹对象
        /// </summary>
        /// <param name="sourcepath">包含邮件集的目录路径</param>
        /// <param name="has_head">邮件是否含有头信息</param>
        /// <returns></returns>
        public static FingerPrint FromSourceFile(string sourcepath, bool has_head)
        {
            //利用开头前两行得到二进制状态码，顺路得到全部bodies
            var dirs = Directory.GetFiles(sourcepath);
            string nowbody = ""; string allText = "";
            List<FirstLineCode> allcode = new List<FirstLineCode>();
            foreach (var dir in dirs)
            {
                nowbody = IOMethod.GetBody(dir, has_head);
                FirstLineCode nowcode = new FirstLineCode(nowbody);
                allcode.Add(nowcode);
                allText += nowbody;
            }
            //遍历一遍对逗号和句号计数，因为在生成List后，标点信息会被略去
            int dot = 0; int ju = 0;
            for (int i = 0; i < allText.Length; i++)
            {
                if (allText[i] == ',')
                    dot++;
                else if (allText[i] == '.')
                    ju++;
            }
            //再遍历一遍对形容词和词种类计数
            var TextList = IOMethod.GetWordsList(allText);
            int TotalWords = 0; int AdjWords = 0;
            for (int i = 0; i < TextList.Count; i++)
            {
                TotalWords += TextList[i].times;
                if (TextList[i].isAdj)
                {
                    AdjWords += TextList[i].times;
                }
            }
            FirstLineCode thisflc = FirstLineCode.SumAll(allcode);
            double thisadjrate = (((double)AdjWords) / ((double)TotalWords));
            double thiswordrichness = (((double)TextList.Count) / (Math.Sqrt((double)TotalWords)));
            double thislongrichness = (((double)dot) / ((double)ju));
            List<EWord> thishighratewords = TextList;
            FingerPrint result = new FingerPrint(thisflc, thisadjrate, thiswordrichness, thislongrichness, thishighratewords);
            result.SourceMailName = sourcepath;
            return result;
        }
        /// <summary>
        /// 从指纹文件（.mfg）中实例化指纹对象
        /// 指纹文件中：
        /// 第一行：称谓书写习惯状态码
        /// 第二行：形容词使用频率
        /// 第三行：用词丰富性
        /// 第四行：长短句使用线段
        /// 后全部：高频词
        /// </summary>
        public static FingerPrint FromSourceMFGFile(string mfgpath)
        {
            //保证对象被释放
            using (StreamReader sr = new StreamReader(mfgpath))
            {
                int code = Convert.ToInt32(sr.ReadLine());
                FirstLineCode thisflc = new FirstLineCode(code);
                double thisadj = Convert.ToDouble(sr.ReadLine());
                double thiswordrichness = Convert.ToDouble(sr.ReadLine());
                double thislongrichness = Convert.ToDouble(sr.ReadLine());
                int top30limit = 30; string nowline = null;
                var thisewordlist = new List<EWord>();
                while (top30limit > 0 && !(string.IsNullOrEmpty((nowline = sr.ReadLine()))))
                {
                    var ewordarray = nowline.Split(':');
                    EWord noweword = new EWord(Convert.ToInt32(ewordarray[1]), ewordarray[0]);
                    thisewordlist.Add(noweword);
                    top30limit--;
                }
                FingerPrint result = new FingerPrint(thisflc, thisadj, thiswordrichness, thislongrichness, thisewordlist);
                result.SourceMailName = mfgpath;
                return result;
            }
        }
    }
    /// <summary>
    /// 封装指纹实例间的比对方法，并储存比对结果（VarianceResult）
    /// </summary>
    public struct FingerPrintResult
    {
        public FingerPrint a { get; }
        public FingerPrint b { get; }
        public double HowManyEqualInTop30 { get; }
        public double FirstLineCodeResult { get; }
        public double AdjRateResult { get; }
        public double WordRichnessResult { get; }
        public double LongRichnessResult { get; }
        public double HighRateWordsResult { get; }
        //直接反应指纹实例a与b间的相似程度，越小越相似
        public double VarianceResult { get; }
        public string Name { get; }
        /// 指纹文件中：
        /// 第一行：称谓书写习惯状态码
        /// 第二行：形容词使用频率
        /// 第三行：用词丰富性
        /// 第四行：长短句使用线段
        /// 后全部：高频词
        /// </summary>
        public FingerPrintResult(FingerPrint a, FingerPrint b)
        {
            this.a = a;
            this.b = b;
            Name = IOMethod.GetFileName(a.SourceMailName) + "--" + IOMethod.GetFileName(b.SourceMailName);
            
            FirstLineCodeResult = ((double)FirstLineCode.Judge(a.FirstLineCode, b.FirstLineCode));
            FirstLineCodeResult = FirstLineCodeResult / 10;
            FirstLineCodeResult = FirstLineCodeResult * FirstLineCodeResult;
            FirstLineCodeResult = 0.4241 * FirstLineCodeResult;

            AdjRateResult = a.AdjRate - b.AdjRate;
            AdjRateResult = AdjRateResult * 100;
            AdjRateResult = AdjRateResult * AdjRateResult;
            AdjRateResult = 0.2472 * AdjRateResult;

            WordRichnessResult = a.WordRichness - b.WordRichness;
            WordRichnessResult = WordRichnessResult / 10;
            WordRichnessResult = WordRichnessResult * WordRichnessResult;
            WordRichnessResult = 0.1086 * WordRichnessResult;

            LongRichnessResult = a.LongRichness - b.LongRichness;
            LongRichnessResult = LongRichnessResult * LongRichnessResult;
            LongRichnessResult = 0.1520 * LongRichnessResult;

            double howmanyequal = 0;
            foreach (EWord item in a.HighRateWords)
            {
                if (item.isExistIn(b.HighRateWords))
                {
                    howmanyequal++;
                }
            }
            HowManyEqualInTop30 = howmanyequal;
            HighRateWordsResult = (30 - howmanyequal) / 30;
            HighRateWordsResult = HighRateWordsResult * HighRateWordsResult;
            HighRateWordsResult = 0.06806 * HighRateWordsResult;

            VarianceResult = FirstLineCodeResult + AdjRateResult + WordRichnessResult + LongRichnessResult + HighRateWordsResult;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    /// <summary>
    /// 封装了单词，出现频数，是否形容词的结构体
    /// </summary>
    public struct EWord
    {
        public int times;
        public string theword;
        //是否是形容词，在实例化时即判断
        public bool isAdj;
        public EWord(int thistimes, string thisword)
        {
            times = thistimes;
            theword = thisword;
            isAdj = AdjJudge.IsAdj(thisword);
        }
        /// <summary>
        /// 重写了ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{theword}:{times}\r\n";
        }
        public string ToString(double total)
        {
            return $"{theword}:{((double)times) / total}\r\n";
        }
        //判断this是否存在于EWord集合motherlist中
        public bool isExistIn(List<EWord> motherlist)
        {
            int minlimit = Math.Min(30, motherlist.Count - 1);
            for (int i = 0; i <= minlimit; i++)
            {
                if (motherlist[i].theword.Equals(this.theword))
                    return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 解析邮件开头两行模式的状态机
    /// </summary>
    public class FirstLineCode
    {
        /*
         * Code码表示从第一个非空段开始，两段长度内，文章的状态
         * Code码本身是一个10bit二进制数，默认是0
         * 满足以下条件，对对应位加1
         * 1.前20个char内有','(+1)
         * 2.前20个char内有' '(+2)
         * 3.前20个char内有"Hi"(+4)
         * 4.前20个char内有"Dear"(+8)
         * 5.前20个char内有'-'(+16)
         * 6.前20个char中大写字母总数比上小写字母总数>0.8(+32)
         * 7.第1个char是I(+64)
         * 8.第1行char数大于20(+128)
         * 9.第1行最后一个char是':'(+256)
         * 10.第2行为空(+512)
         */
        public int Code = 0;
        public const int bitsNum = 10;
        public FirstLineCode(string body)
        {
            Regex nextline = new Regex("\r\n");
            string[] lines = nextline.Split(body);
            int FirstNonNullLineIndex = 0;
            for (; FirstNonNullLineIndex < lines.Length; FirstNonNullLineIndex++)
            {
                if (!string.IsNullOrEmpty(lines[FirstNonNullLineIndex]))
                {
                    break;
                }
                if (FirstNonNullLineIndex == lines.Length - 1)
                {
                    return;
                }
            }
            handlefirstline(lines[FirstNonNullLineIndex]);
            if (lines.Length <= FirstNonNullLineIndex + 1)
            {
                handlefirstline(null);
            }
            else
            {
                handlesecondline(lines[FirstNonNullLineIndex + 1]);
            }
        }
        public FirstLineCode(int code)
        {
            this.Code = code;
        }
        private void handlefirstline(string firstline)
        {
            #region 前期判断
            int len = Math.Min(20, firstline.Length);
            string FirstTwentiesChar = firstline.Substring(0, len);
            bool hasdot = false;
            bool hasspace = false;
            bool has_ = false;
            Regex regex_hasHi = new Regex("Hi");
            Regex regex_hasDear = new Regex("Dear");
            Regex regex_upperlitter = new Regex("[A-Z]");
            Regex regex_lowerlitter = new Regex("[a-z]");
            double upperlitter = 0, lowerlitter = 0;
            for (int i = 0; i < len; i++)
            {
                char now = FirstTwentiesChar[i];
                if (regex_lowerlitter.IsMatch(now.ToString()))
                {
                    lowerlitter += 1.0;
                }
                else if (regex_upperlitter.IsMatch(now.ToString()))
                {
                    upperlitter += 1.0;
                }
                switch (now)
                {
                    case '.':
                        hasdot = true;
                        break;
                    case ' ':
                        hasspace = true;
                        break;
                    case '_':
                        has_ = true;
                        break;
                    default:
                        break;
                }
            }
            #endregion
            #region 计算Code值
            if (hasdot)
                Code += 1;
            if (hasspace)
                Code += 2;
            if (has_)
                Code += 16;
            if (regex_hasDear.IsMatch(FirstTwentiesChar))
                Code += 8;
            if (regex_hasHi.IsMatch(FirstTwentiesChar))
                Code += 4;
            if (upperlitter / lowerlitter > 0.8)
                Code += 32;
            if (firstline[0] == 'I')
                Code += 64;
            if (len == 20)
                Code += 128;
            if (firstline[firstline.Length - 1] == ':')
                Code += 256;
            #endregion
        }
        private void handlesecondline(string secondline)
        {
            if (string.IsNullOrEmpty(secondline))
            {
                Code += 512;
            }
        }
        private int getabit(int whichbit)
        {
            return (Code >> whichbit) % 2;
        }
        //这里所有的bits都是低索引在低位
        private static int getcode(int[] bits)
        {
            int result = 0;
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                result = result << 1;
                result += bits[i];
            }
            return result;
        }
        static public FirstLineCode SumAll(List<FirstLineCode> allcodes)
        {
            int count = allcodes.Count;
            int _30percent = (int)(count * .7);
            int[] bits = new int[bitsNum];
            foreach (var code in allcodes)
            {
                for (int i = 0; i < bitsNum; i++)
                {
                    bits[i] += code.getabit(i);
                }
            }
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] > _30percent)
                    bits[i] = 1;
                else
                    bits[i] = 0;
            }
            return new FirstLineCode(getcode(bits));
        }
        public string ToHumanString()
        {
            string result = Convert.ToString(Code, 2);
            while (result.Length < bitsNum)
            {
                result = $"0{ result }";
            }
            return result;
        }
        public override string ToString()
        {
            return Code.ToString();
        }
        public static int Judge(FirstLineCode a, FirstLineCode b)
        {
            string sa = a.ToHumanString();
            string sb = b.ToHumanString();
            int result = 0;
            for (int i = 0; i < sa.Length; i++)
            {
                if (sa[i] == sb[i])
                    result++;
            }
            return 10 - result;
        }
    }
    /// <summary>
    /// 封装了判断是否是形容词的方法，与形容词标准后缀字典
    /// </summary>
    public class AdjJudge
    {
        //包含了形容词标准后缀
        static public string[] Suffixs = { "able", "ible", "al", "ant", "ar", "ary",
            "ful", "ic", "ical", "ive", "less", "ous", "some", "ward" };
        public static bool IsAdj(string input)
        {
            foreach (var suffix in Suffixs)
            {
                //遍历字典，生成正则表达式，判断是否符合形容词标准
                Regex regex_adj = new Regex("[A-Za-z]{3}" + suffix + "$");
                if (regex_adj.IsMatch(input))
                    return true;
            }
            return false;
        }
    }
    /// <summary>
    /// 为方便在matlab中进行函数拟合的封装
    /// </summary>
    public struct Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
