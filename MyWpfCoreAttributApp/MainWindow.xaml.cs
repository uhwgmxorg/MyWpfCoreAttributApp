using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace MyWpfCoreAttributApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region INotify Changed Properties  
        private string message;
        public string Message
        {
            get { return message; }
            set { SetField(ref message, value, nameof(Message)); }
        }
        private string output;
        public string Output
        {
            get { return output; }
            set { SetField(ref output, value, nameof(Output)); }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

#if DEBUG
            Title += "    Debug Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
#else
            Title += "    Release Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
#endif
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button_1_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            Message = "You pressed Button #1";
            Console.Beep(785, 240);

            var obj = new AA();
            Output += obj.GetAllMyAttributeAsString<AA>();
        }

        /// <summary>
        /// Button_2_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            Message = "You pressed Button #2";
            Console.Beep(785, 240);

            var obj = new BB();
            Output += obj.GetAllMyAttributeAsString<BB>();
        }

        /// <summary>
        /// Button_3_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            Message = "You pressed Button #3";
            Console.Beep(785, 240);

            var obj = new CC();
            Output += obj.GetAllMyAttributeAsString<CC>();
        }

        /// <summary>
        /// Button_4_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            Message = "You pressed Button #4";
            Console.Beep(785, 240);

            var obj = new B();
            Output += obj.GetAllMyAttributeAsString<B>();
        }

        /// <summary>
        /// Button_5_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            Message = "You pressed Button #5";
            Console.Beep(785, 240);

            var obj = new D();
            Output += obj.GetAllMyAttributeAsString<D>();

            IEnumerable<string> permissions = typeof(D).GetCustomAttributes(typeof(MyPermissionAttribute), true)
                                                       .Cast<MyPermissionAttribute>()
                                                       .Select(x => x.Permission);
            Output += "the [MyPermissionAttribute] propery list of class D is" + "\n";
            foreach (var p in permissions)
                Output += p + "\n";
        }

        /// <summary>
        /// Button_Clear_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            Message = "You pressed Clear";
            Console.Beep(300, 240);

            Output = "";
        }

        /// <summary>
        /// Button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// Lable_Message_MouseDown
        /// Clear Message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lable_Message_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Message = "";
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// SetField
        /// for INotify Changed Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }

    [MyAttribute11]
    public class AA : A
    {
        public AA()
        {
        }
    }

    [MyAttribute22]
    public class BB : B
    {
        public BB()
        {
        }
    }

    [MyAttribute11]
    [MyAttribute1]
    public class CC : C
    {
        public CC()
        {
        }
    }

    [MyAttribute1]
    public class A : Base
    {
        string _content = "content";
        public A()
        {
            string c;
            c = _content;
        }
    }

    public class B : Base
    {
    }

    [MyAttribute33]
    [MyAttribute3]
    public class C : Base
    {
    }

    [MyPermissionAttribute("Admin")]
    [MyPermissionAttribute("Operator")]
    [MyPermissionAttribute("User")]
    public class D : Base
    {
    }

    public class Base : IBase
    {
        public string GetAllMyAttributeAsString<T>() where T : new()
        {
            string myAttribut = "";

            var obj = new T() as IBase;
            foreach (var at in ((IBase)obj).GetAllMyAttribut<T>())
                myAttribut += at + "\n";

            return myAttribut;
        }

        public List<string> GetAllMyAttribut<T>()
        {
            List<string> myAttributList = new List<string>();
            TypeInfo typeInfo = typeof(T).GetTypeInfo();
            var attrs = typeInfo.GetCustomAttributes();
            foreach (var attr in attrs)
                myAttributList.Add($"attribute [{attr.GetType().Name}] on class {GetType().Name}");

            if(myAttributList.Count == 0)
                myAttributList.Add($"we have no attibut for class {GetType().Name}\n");
            else
                myAttributList.Add($"we have {myAttributList.Count} attibut for class {GetType().Name}\n");

            return myAttributList;
        }
    }

    public interface IBase
    {
        public string GetAllMyAttributeAsString<T>() where T : new();
        public List<string> GetAllMyAttribut<T>();

    }
}
