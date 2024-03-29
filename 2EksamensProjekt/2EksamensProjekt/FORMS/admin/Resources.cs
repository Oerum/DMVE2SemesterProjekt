﻿namespace _2EksamensProjekt.FORMS.admin;

public partial class Resources : Form
{
    private readonly API _api = API.GetInstance();
    private readonly API.SQLCMDS _sqlCMDS = API.SQLCMDS.GetInstance();
    private static readonly Resources Singleton = new();

    private Resources()
    {
        InitializeComponent();
        radioButton3.Checked = true;
        radioButton6.Checked = true;
        radioButton8.Checked = true;
        button3.Enabled = false;
        comboBox3.Enabled = false;
        comboBox1.Enabled = false;
        comboBox2.Enabled = false;
        comboBox4.Enabled = false;
        comboBox5.Enabled = false;
        radioButton3.Enabled = true;
        comboBox1.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ff");
        Task t2 = new(Worker);
        t2.Start();
    }

    public static Resources GetInstance()
    {
        return Singleton;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
        
    }

    private void Worker()
    {
        do
        {
            try
            {
                //Invokers
                if (radioButton8.Checked) //Sort
                {
                    _api.GroupBoxInvoker(groupBox1, true);


                    if (radioButton1.Checked) // Per User
                    {
                        _api.ComboBoxInvoker(comboBox3, true);
                        _api.ComboBoxInvoker(comboBox1, true);
                        _api.ComboBoxInvoker(comboBox2, true);
                        _api.ButtonInvoker(button3, false);
                        _api.ComboBoxInvoker(comboBox4, false);

                    }

                    if (radioButton2.Checked) // Per Unit
                    {
                        _api.ComboBoxInvoker(comboBox4, true);
                        _api.ComboBoxInvoker(comboBox1, false);
                        _api.ComboBoxInvoker(comboBox2, false);
                        _api.ComboBoxInvoker(comboBox3, false);

                    }

                    if (radioButton6.Checked || radioButton7.Checked) //All
                    {
                        _api.ComboBoxInvoker(comboBox3, false);
                        _api.ComboBoxInvoker(comboBox4, false);
                        _api.ComboBoxInvoker(comboBox1, false);
                        _api.ComboBoxInvoker(comboBox2, false);
                        _api.ComboBoxInvoker(comboBox4, false);
                        _api.ComboBoxInvoker(comboBox5, false);

                        if (radioButton7.Checked)
                        {
                            _api.GroupBoxInvoker(groupBox2, true);
                            _api.GroupboxReader(groupBox2, API.SetReaderField.AvailableType);
                        }
                    }
                }

                if (radioButton9.Checked) //Book
                {
                    _api.ButtonInvoker(button3, true);
                    _api.ComboBoxInvoker(comboBox2, false);
                    _api.ComboBoxInvoker(comboBox3, true);
                    _api.ComboBoxInvoker(comboBox5, true);
                    _api.ComboBoxInvoker(comboBox1, true);
                    _api.ComboBoxInvoker(comboBox4, true);
                    _api.GroupBoxInvoker(groupBox1, false);
                    _api.GroupBoxInvoker(groupBox2, true);
                }

                //Fillers
                if (radioButton3.Checked) //WashingMachines
                {
                    if (radioButton8.Checked)//Sort
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.WMSORTALL));
                    }
                    else if (radioButton9.Checked)//Book
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    }
                    _api.ComboBoxFillNoSqlInt(comboBox5, 4);
                }
                else if (radioButton4.Checked) //PartyHall
                {
                    if (radioButton8.Checked)//Sort
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.PHSortAll));
                    }
                    else if (radioButton9.Checked)//Book
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    }
                    _api.ComboBoxFillNoSqlInt(comboBox5, 24);
                }
                else if (radioButton5.Checked) // ParkingSpace
                {
                    if (radioButton8.Checked)//Sort
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.PSSortAll));
                    }
                    else if (radioButton9.Checked)//Book
                    {
                        _api.ComboBoxFill(comboBox4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourceIDS));
                    }
                    _api.ComboBoxFillNoSqlInt(comboBox5, 48);
                }
                
                _api.ComboBoxFill(comboBox3, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.Usernames));
                _api.ComboBoxFill(comboBox1, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.StartDate));
                _api.ComboBoxFill(comboBox2, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.EndDate));
                _api.ComboBoxFill(comboBox6, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.BookingCancelIDs));

                //Readers
                _api.GroupboxReader(groupBox2, API.SetReaderField.AvailableType);
                _api.ComboBoxReader(comboBox1, API.SetReaderField.Start);
                _api.ComboBoxReader(comboBox2, API.SetReaderField.End);
                _api.ComboBoxReader(comboBox3, API.SetReaderField.SortUsername);
                _api.ComboBoxReader(comboBox4, API.SetReaderField.UnitID);
                _api.ComboBoxReader(comboBox5, API.SetReaderField.Duration);
                _api.ComboBoxReader(comboBox6, API.SetReaderField.CancelBookingID);

                //GridViewFillers
                _api.Gridview(dataGridView4, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AllResourcesBooked), true, DataGridViewAutoSizeColumnMode.Fill);
                _api.Gridview(dataGridView1, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.AvailableResourcesByType), true, DataGridViewAutoSizeColumnMode.Fill);

                if (radioButton8.Checked)
                {
                    if (radioButton6.Checked || radioButton10.Checked) //All User
                    {
                        _api.Gridview(dataGridView5, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.ResourceSortAllUsers), true, DataGridViewAutoSizeColumnMode.AllCells);

                    }
                    else if (radioButton7.Checked) //All Per Unit (Count)
                    {
                        _api.Gridview(dataGridView5, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.ResourceSortAllPerUnit), true, DataGridViewAutoSizeColumnMode.Fill);
                    }
                    else if (radioButton1.Checked) //Per User
                    {
                        _api.Gridview(dataGridView5, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.ResourceSortPerUser), true, DataGridViewAutoSizeColumnMode.Fill);
                    }
                    else if (radioButton2.Checked) //Per Unit
                    {
                        _api.Gridview(dataGridView5, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.ResourceSortPerUnit), true, DataGridViewAutoSizeColumnMode.Fill);
                    }
                    else if (radioButton10.Checked) //Print Everything
                    {
                        _api.Gridview(dataGridView5, _sqlCMDS.GetSQLQuery(API.SQLCMDS.SELECTSQLQUERY.ResourceSortAllUsers), true, DataGridViewAutoSizeColumnMode.Fill);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }
        while (true);
    }

    private void button3_Click(object sender, EventArgs e)
    {
        _api.Booking();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (radioButton6.Checked) //All User
            {
                _api.AdminStatisticsPrint(API.ResourceSort.AllUsers);
            }
            else if (radioButton7.Checked) //All Per Unit (Count)
            {
                _api.AdminStatisticsPrint(API.ResourceSort.AllPerUnit);
            }
            else if (radioButton1.Checked) //Per User
            {
                _api.AdminStatisticsPrint(API.ResourceSort.PerUser);
            }
            else if (radioButton2.Checked) //Per Unit
            {
                _api.AdminStatisticsPrint(API.ResourceSort.PerUnit);
            }
            else if (radioButton10.Checked) //Everything
            {
                _api.AdminStatisticsPrint(API.ResourceSort.Everything);
            }
        }
        catch (Exception)
        {
            if (radioButton1.Checked)
            {
                MessageBox.Show($@"User
                                Start Date
                                End Date
                                Unit Type
                                ARE REQUIRED TO SORT BY USER");
            }
            else if (radioButton2.Checked)
            {
                MessageBox.Show($@"Start Date
                                End Date
                                Unit Type 
                                Unit ID
                                ARE REQUIRED TO SORT BY UNIT");
            }
        }
    }

    private void button7_Click(object sender, EventArgs e)
    {
        if (comboBox6.Text == String.Empty)
        {
            MessageBox.Show(@"Select ID");
        }
        else
        {
            _api.CancelReservation();
        }
    }
}