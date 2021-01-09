using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace UI
{
    class Charts
    {
        List<bool> SelectedStatus;
        public Canvas NewBackground = new Canvas();
        double TotalAngel = 0;
        System.Windows.Point MouseLeftBtnDownLocation;
        SolidColorBrush OldBrush;
        System.Windows.Media.Color EnterColor;
        System.Windows.Media.Color SelectColor;
        List<Pairs> AllPaths = new List<Pairs>();
        public void DrawPieChart(ref Canvas Background, List<TriadPairs>Datas, System.Windows.Point Center , double InnerRaduse , double OuterRaduse , SolidColorBrush StrokeBrush ,
            double StrokeThickness , System.Windows.Media.Color MouseEnterColor, System.Windows.Media.Color SelectionColor,System.Windows.Size RectColorSize,
            double TagItemsWidth,string Mesure)
        {
            SelectedStatus = new List<bool>();
            double TotolaValues = 0;
            ScrollViewer scrollViewer = new ScrollViewer();
            StackPanel AllTagsSP = new StackPanel();
            NewBackground = Background;
            EnterColor = MouseEnterColor;
            SelectColor = SelectionColor;
            Background.Dispatcher.Invoke(() =>
            {
                
                
                for(int i = 0; i < Datas.Count; i++)
                {
                    TotolaValues += Datas[i].Value;
                }
                for(int i = 0; i < Datas.Count;i++)
                {
                    System.Windows.Shapes.Path SlicePath = new System.Windows.Shapes.Path();
                    PathGeometry SliceGeometry = new PathGeometry();
                    PathFigure SliceFigure = new PathFigure();
                    LineSegment RightLine = new LineSegment();
                    ArcSegment LargeArc = new ArcSegment();
                    LineSegment LeftLine = new LineSegment();
                    ArcSegment SmallArc = new ArcSegment();
                    double Degree = (((Datas[i].Value / TotolaValues) * 2 *Math.PI)) ;
                    if (Degree == 2 * Math.PI)
                    {
                        Degree -= 0.0000001;
                    }
                    //double Degree = Math.PI + TotalAngel;
                    if (Degree > Math.PI)
                    {
                        LargeArc.IsLargeArc = true;
                        SmallArc.IsLargeArc = true;
                    }
                    else
                    {
                        LargeArc.IsLargeArc = false;
                        SmallArc.IsLargeArc = false;
                    }
                    Degree += TotalAngel;
                    SliceFigure.StartPoint = new System.Windows.Point(Center.X + (Math.Cos(TotalAngel) * InnerRaduse), Center.Y - (Math.Sin(TotalAngel) * InnerRaduse));
                    RightLine.IsStroked = true;
                    RightLine.Point = new System.Windows.Point(Center.X + (Math.Cos(TotalAngel) * OuterRaduse), Center.Y - (Math.Sin(TotalAngel) * OuterRaduse));
                    SliceFigure.Segments.Add(RightLine);
                    LargeArc.Size = new System.Windows.Size(OuterRaduse, OuterRaduse);
                    LargeArc.SweepDirection = SweepDirection.Counterclockwise;
                    LargeArc.IsStroked = true;
                    LargeArc.RotationAngle = 0;
                    LargeArc.Point = new System.Windows.Point(Center.X + (Math.Cos(Degree)) * OuterRaduse, Center.Y - (Math.Sin(Degree)) * OuterRaduse);
                    SliceFigure.Segments.Add(LargeArc);
                    LeftLine.IsStroked = true;
                    LeftLine.Point = new System.Windows.Point(Center.X - (Math.Cos(Degree) * InnerRaduse) + (Math.Cos(Degree) * OuterRaduse) , Center.Y + (Math.Sin(Degree)) * InnerRaduse - (Math.Sin(Degree)) * OuterRaduse);
                    SliceFigure.Segments.Add(LeftLine);
                    SmallArc.Size = new System.Windows.Size(InnerRaduse, InnerRaduse);
                    SmallArc.SweepDirection = SweepDirection.Clockwise;
                    SmallArc.IsStroked = true;
                    SmallArc.RotationAngle = 0;
                    SmallArc.Point = new System.Windows.Point(Center.X + (Math.Cos(TotalAngel) * InnerRaduse), Center.Y - (Math.Sin(TotalAngel) * InnerRaduse));
                    SliceFigure.Segments.Add(SmallArc);
                    SliceGeometry.Figures.Add(SliceFigure);
                    SlicePath.Data = SliceGeometry;
                    SlicePath.Stroke = StrokeBrush;
                    SlicePath.StrokeThickness = StrokeThickness;
                    SlicePath.Fill = new SolidColorBrush(Datas[i].Color);
                    SlicePath.MouseEnter += SlicePath_MouseEnter;
                    SlicePath.MouseLeave += SlicePath_MouseLeave;
                    SlicePath.MouseLeftButtonDown += SlicePath_MouseLeftButtonDown;
                    SlicePath.MouseLeftButtonUp += SlicePath_MouseLeftButtonUp;
                    NewBackground.Children.Add(SlicePath);
                    Pairs NewPaire = new Pairs();
                    NewPaire.SlicePath = SlicePath;
                    NewPaire.ISSelected = false;
                    NewPaire.Color = Datas[i].Color;
                    AllPaths.Add(NewPaire);
                    TotalAngel += (0.00001+ Degree - TotalAngel);
                    Border ItemBorder = new Border();
                    DockPanel ItemDock = new DockPanel();
                    StackPanel DataSP = new StackPanel();
                    StackPanel NumberDataSP = new StackPanel();
                    Border ColorBorder = new Border();
                    TextBlock TagTB = new TextBlock();
                    TextBlock ValueTB = new TextBlock();
                    TextBlock PercentTB = new TextBlock();
                    ItemBorder.Margin = new Thickness(0, 5, 0, 5);
                    ItemBorder.MaxWidth = TagItemsWidth;
                    ItemBorder.MinWidth = TagItemsWidth;
                    ItemDock.Width = TagItemsWidth;
                    ItemBorder.CornerRadius = new CornerRadius(5);
                    ItemBorder.Background = new SolidColorBrush(Colors.LightGray);
                    ItemBorder.BorderThickness = new Thickness(1);
                    DataSP.Orientation = Orientation.Horizontal;
                    NumberDataSP.Orientation = Orientation.Horizontal;
                    NumberDataSP.Margin = new Thickness(0, 2, 3, 0);
                    NumberDataSP.HorizontalAlignment = HorizontalAlignment.Right;
                    NumberDataSP.VerticalAlignment = VerticalAlignment.Top;
                    ColorBorder.Background = new SolidColorBrush(Datas[i].Color);
                    ColorBorder.CornerRadius = new CornerRadius(5);
                    ColorBorder.Width = RectColorSize.Width;
                    ColorBorder.Height = RectColorSize.Height;
                    ColorBorder.Margin = new Thickness(4, 0, 0, 0);
                    DataSP.Children.Add(ColorBorder);
                    TagTB.Text = Datas[i].Tag;
                    TagTB.HorizontalAlignment = HorizontalAlignment.Left;
                    TagTB.VerticalAlignment = VerticalAlignment.Top;
                    TagTB.Margin = new Thickness(5, 2, 0, 2);
                    DataSP.Children.Add(TagTB);
                    ValueTB.Text = Datas[i].Value.ToString() +" "+ Mesure;
                    ValueTB.HorizontalAlignment = HorizontalAlignment.Right;
                    ValueTB.VerticalAlignment = VerticalAlignment.Top;
                    ValueTB.Margin = new Thickness(0, 2, 5, 0);
                    PercentTB.Text = Math.Round(((Datas[i].Value / TotolaValues) * 100),4).ToString() + "%";
                    PercentTB.HorizontalAlignment = HorizontalAlignment.Right;
                    PercentTB.VerticalAlignment = VerticalAlignment.Top;
                    PercentTB.Margin = new Thickness(0, 2, 20, 0);
                    NumberDataSP.Children.Add(PercentTB);
                    NumberDataSP.Children.Add(ValueTB);
                    ItemDock.Children.Add(DataSP);
                    ItemDock.Children.Add(NumberDataSP);
                    ItemBorder.Child = ItemDock;
                    AllTagsSP.Children.Add(ItemBorder);
                    SelectedStatus.Add(false);
                }
            });
            
            AllTagsSP.HorizontalAlignment = Background.HorizontalAlignment;
            AllTagsSP.VerticalAlignment = Background.VerticalAlignment;
            Background = NewBackground;
            AllTagsSP.Orientation = Orientation.Vertical;
            scrollViewer.Margin = new Thickness(2* OuterRaduse + 15, 10, 5, 5);
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            AllTagsSP.Uid = "AllTagsSP";
            AllTagsSP.MinHeight = OuterRaduse;
            //AllTagsSV.MaxWidth = OuterRaduse;
            AllTagsSP.MinWidth = 10;
            //AllTagsSP.MaxHeight = 50;
            scrollViewer.Content = AllTagsSP;
            scrollViewer.MaxHeight = 200;
            Background.Children.Add(scrollViewer);
        }

        private void SlicePath_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Path Target = sender as System.Windows.Shapes.Path;
            if(e.GetPosition(Target) == MouseLeftBtnDownLocation)
            {
                
                int ListElementIndex = AllPaths.IndexOf(AllPaths.Find(x => x.SlicePath == Target));
                Pairs ListElement = AllPaths.Find(x => x.SlicePath == Target);
                if (AllPaths[ListElementIndex].ISSelected == false)
                {
                    StackPanel TargetSP = (StackPanel)NewBackground.Children.Cast<UIElement>().First(x => x.Uid == "AllTagsSP");
                    Border TargetBorder = (Border)TargetSP.Children[ListElementIndex];
                    for (int i = 0; i < SelectedStatus.Count; i++)
                    {
                        if (SelectedStatus[i] == true)
                        {
                            Border TempBorder = (Border)TargetSP.Children[i];
                            SelectedStatus[i] = false;
                            Pairs Temp = AllPaths[i];
                            Temp.ISSelected = false;
                            Temp.SlicePath.Fill = new SolidColorBrush(ListElement.Color);
                            Temp.SlicePath.MouseLeave += SlicePath_MouseLeave;
                            Temp.SlicePath.MouseEnter += SlicePath_MouseEnter;
                            AllPaths[i] = Temp;
                            TempBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
                            TargetSP.Children[i] = TempBorder;
                        }
                    }
                    ListElement.ISSelected = true;
                    SelectedStatus[ListElementIndex] = true;
                    AllPaths[ListElementIndex] = ListElement;
                    TargetBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                    //TargetBorder.BorderThickness = new Thickness(1);
                    TargetSP.Children[ListElementIndex] = TargetBorder;
                    Target.Fill = new SolidColorBrush(SelectColor);
                    Target.MouseLeave -= SlicePath_MouseLeave;
                    Target.MouseEnter -= SlicePath_MouseEnter;
                }
                else
                {
                    ListElement.ISSelected = false;
                    SelectedStatus[ListElementIndex] = false;
                    AllPaths[ListElementIndex] = ListElement;
                    StackPanel TargetSP = (StackPanel)NewBackground.Children.Cast<UIElement>().First(x => x.Uid == "AllTagsSP");
                    Border TargetBorder = (Border)TargetSP.Children[ListElementIndex];
                    TargetBorder.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    //TargetBorder.BorderThickness = new Thickness(1);
                    TargetSP.Children[ListElementIndex] = TargetBorder;
                    Target.Fill = new SolidColorBrush(ListElement.Color);
                    OldBrush = new SolidColorBrush(ListElement.Color);
                    Target.MouseLeave += SlicePath_MouseLeave;
                    Target.MouseEnter += SlicePath_MouseEnter;
                }
            }
        }

        private void SlicePath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Shapes.Path Target = sender as System.Windows.Shapes.Path;
            MouseLeftBtnDownLocation = e.GetPosition(Target);
        }

        private void SlicePath_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Windows.Shapes.Path Target = sender as System.Windows.Shapes.Path;
            OldBrush = (SolidColorBrush) Target.Fill;
            Target.Fill = new SolidColorBrush(EnterColor);
        }

        private void SlicePath_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Windows.Shapes.Path Target = sender as System.Windows.Shapes.Path;
            Target.Fill = OldBrush;
        }

        public struct TriadPairs
        {
            public double Value;
            public string Tag;
            public System.Windows.Media.Color Color;
        }
        struct Pairs
        {
            public System.Windows.Shapes.Path SlicePath;
            bool IsSelected;
            public System.Windows.Media.Color Color;
            public bool ISSelected
            {
                get
                {
                    return IsSelected;
                }
                set
                {
                    if(value == false)
                    {
                        IsSelected = value;
                    }
                    else
                    {
                        IsSelected = value;
                    }
                }
            }
        }
    }
}
