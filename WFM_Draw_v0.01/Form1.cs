using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFM_Draw_v0._01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.pictureBox1.CreateGraphics();//创建GDI对像

            //创建画笔(颜色)
            Pen npen = new Pen(Brushes.Red);

            //创建两个点
            PointF n1 = new PointF(532169.626857493F, 3525216.34303642F);
            PointF n2 = new PointF(532170.50108954F, 3525209.77448995F);
            PointF n3 = new PointF(532158.942205515F, 3525208.2360755F);
            PointF n4 = new PointF(532158.067998938F, 3525214.8044306F);
            PointF n5 = new PointF(532169.626857493F, 3525216.34303642F);

            PointF[] pointFs = { point2New(n1, n1) , point2New(n2, n1), point2New(n3, n1), point2New(n4, n1), point2New(n1, n1) };

            g.DrawPolygon(npen, pointFs);
            //  g.FillRectangle();
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            g.FillClosedCurve(blueBrush, pointFs, FillMode.Winding,0.01F);
            string sLengtg = GetLength(n1, n2).ToString();

            //旋转测试1
            g.SmoothingMode = SmoothingMode.Default;

            string tempstr = "章松山";
            SizeF f = g.MeasureString(tempstr, new Font("宋体", 9));

            g.TranslateTransform(f.Height, 0);//偏移量
            g.RotateTransform(45);

            Brush myBrush = Brushes.Blue;
            g.DrawString(tempstr, this.Font, myBrush, 0, 0);
          //g.DrawRectangle(new Pen(Color.Red), 0, 0, f.Width, f.Height);
            g.ResetTransform();





            //旋转测试2
         Font _font = new Font("Arial", 12);
         Brush _brush = new SolidBrush(Color.Black);
         Pen _pen = new Pen(Color.Black, 1f);
         string _text = "Crow Soft";
         GraphicsText graphicsText = new GraphicsText();
         graphicsText.Graphics = this.pictureBox1.CreateGraphics();//创建GDI对像

            // 绘制围绕点旋转的文本
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            float ag = Convert.ToSingle( Poing2Angle(n1,n2));
            graphicsText.DrawString(_text, _font, _brush, new PointF(((point2New(n1, n1).X + point2New(n2, n1).X)) / 2, (point2New(n1, n1).Y + point2New(n2, n1).Y) / 2), format, ag);

            g.DrawString(sLengtg, new Font("宋体", 12, FontStyle.Bold), Brushes.Blue, new PointF(((point2New(n1, n1).X+ point2New(n2, n1).X))/2, (point2New(n1, n1).Y+ point2New(n2, n1).Y)/2));

        }

        /// <summary>
        /// 移动将A，移动B个位置，放大
        /// </summary>
        /// <param name="A"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private PointF point2New(PointF paf,PointF pbf)
        {
           
            PointF pnf = new PointF(paf.X - pbf.X+20, paf.Y - pbf.Y+10);   //移动位置

            PointF pnf2 = new PointF(pnf.X*15, pnf.Y*15);   //放大

            return pnf2;
        }

        /// <summary>
        /// 计算2点点间的距离。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double GetLength(PointF a , PointF b)
        {
            double leng = 0;
            leng = Math.Sqrt(Math.Pow((a.X-b.X),2)+Math.Pow((a.Y-b.Y),2));
            return leng;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bit = new Bitmap(this.Width, this.Height);//实例化一个和窗体一样大的bitmap
            Graphics g = Graphics.FromImage(bit);
            g.CompositingQuality = CompositingQuality.HighQuality;//质量设为最高
            g.CopyFromScreen(this.Left, this.Top, 0, 0, new Size(this.Width, this.Height));//保存整个窗体为图片
            //g.CopyFromScreen(panel游戏区 .PointToScreen(Point.Empty), Point.Empty, panel游戏区.Size);//只保存某个控件（这里是panel游戏区）
            bit.Save("weiboTemp.png");//默认保存格式为PNG，保存成jpg格式质量不是很好
        }


        /// <summary>
        ///  根据余弦定理求两个线段夹角  
        /// </summary>
        /// <param name="o"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public double Poing2Angle(PointF o, PointF s)
        {
            double angleOfLine = Math.Atan2((s.Y - o.Y), (s.X - o.X))  * 180 / Math.PI;
            return angleOfLine;
        }

    }
    
}
