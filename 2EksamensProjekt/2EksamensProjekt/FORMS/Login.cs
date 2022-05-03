namespace _2EksamensProjekt.FORMS;

public partial class Login : Form
{
    private readonly API api = API.Getinstance(); 

    private static readonly Login singleton = new Login();

    private Login()
    {
        InitializeComponent();
        Task slogan = new Task(() => Slogan());
        slogan.Start(); //Create an instance of a Task & Start it
    }

    public static Login GetInstance() //Login Form Made Singleton Due To Otherwise Disposion Of Objects --Garbage Collector--.
    {
        return singleton;
    }

/*
    private void formClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        Environment.Exit(0);
    }
*/

    private async void Slogan()
    {
        do
        { 
            if (label1.IsDisposed == false)
            {
                if (label1.InvokeRequired)
                {
                    label1.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to label1
                    {
                        label1.Text = api.SloganT().Result; //Calling Async Task SloganT Method From Api Class.
                    });
                    await Task.Delay(1000); //Sleeping For X Seconds.
                }
            }
            else
            {
                label1.CreateControl();
            }
        }
        while (Form.ActiveForm == Login.ActiveForm); //Keep Task Running While Login Form Is The ActiveForm
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string result = api.Login(textBox1.Text, textBox2.Text).Result; //Calling Async Task Login Method From DAL Class.
        //MessageBox.Show(result);
        if (result == "admin")
        {
            this.Hide();
            adminSP obj = adminSP.GetInstance();
            //obj.Closed += (s, args) => this.Close();
            obj.Show();
        }
        else if (result == "secretary")
        {
            this.Hide();
            secretarySP obj = secretarySP.GetInstance();
            //obj.Closed += (s, args) => this.Close();
            obj.Show();
        }
        else if (result == "youth" || result == "senior" || result == "normal")
        {
            this.Hide();
            residentSP obj = residentSP.GetInstance();
            //obj.Closed += (s, args) => this.Close();
            obj.Show();
        }
            
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (textBox1.Text != String.Empty)
        {
            MessageBox.Show(api.GetPassword(textBox1.Text).Result);
        }
        else
        {
            MessageBox.Show("Enter Username");
        }
    }
}