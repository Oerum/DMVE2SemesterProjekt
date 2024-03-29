namespace _2EksamensProjekt.DAL;
public class API
{
    #region Properties
    private int Min { get; set; }
    private int Max { get; set; }
    public string? AccountUsername { get; set; }
    private string? SortUsername { get; set; }
    private string? NewAccountUsername { get; set; }
    private string? CreateAccountUsername { get; set; }
    private string? HouseID { get; set; }
    private string? AccountName { get; set; }
    private string? AccountSurname { get; set; }
    private string? AccountPhoneNumber { get; set; }
    //public string? User { get; set; }
    private DateTime Start { get; set; }
    private DateTime End { get; set; }
    private DateTime Duration { get; set; }
    private int UnitID { get; set; }
    private int CancelBookingID { get; set; }
    private string? AvailableType { get; set; }
    private string? Password { get; set; }
    private string? WaitlistType { get; set; }
    private string? DeleteFromSystemUsername { get; set; }
    private string? HouseType { get; set; }
    private int M2 { get; set; }
    private int Price { get; set; }
    private string? Address { get; set; }
    private string? Zipcode { get; set; }

    #endregion Fields

    #region Singleton

    private static readonly API Singleton = new(); 
    private API() { } //Private Due to Singleton ^^ 

    //Singleton 
    public static API GetInstance()
    {
        return Singleton;
    }
    #endregion Singleton

    #region SQLCMDS
    private readonly SQLCMDS _sqlCMDS = SQLCMDS.GetInstance();

    internal class SQLCMDS
    {
        private static readonly SQLCMDS Singleton = new();
        private SQLCMDS() { }
        public static SQLCMDS GetInstance()
        {
            return Singleton;
        }

        public enum SELECTSQLQUERY
        {
            //Waitlist
            Waitlist = 1,
            CurrentResidents,
            CurrentResidentsUsername,
            WMSORTALL,
            PHSortAll,
            PSSortAll,
            ResourceSortAllUsers,
            ResourceSortAllPerUnit,
            ResourceSortPerUser,
            ResourceSortPerUnit,
            AllResourcesBooked,
            AvailableResourceIDS,
            AvailableResourcesByType,
            Usernames,
            StartDate,
            EndDate,
            BookingCancelIDs,
            CurrentResidentInfo,
            AvailableHouseSortAll,
            AvailableHouseSortByM2,
            AvailableHouseSortByPrice,
            //Fillers
            AvailableHouseIDs,
            UsernamesOnWaitinglist,
            Zipcode,
        }

        public string GetSQLQuery(SELECTSQLQUERY query)
        {
            switch(query)
            {
                case SELECTSQLQUERY.Waitlist: 
                    return
                        "SELECT a.username AS 'Brugernavn', a.`type`AS 'Type', CONCAT(a.first_names, ' ', a.last_name) AS 'Fuldt navn', phone_number" +
                        "\nFROM account a " +
                        "\nWHERE a.privilege = 'waitlist' " +
                        "\nORDER BY a.creation_date;";
                
                case SELECTSQLQUERY.CurrentResidents: 
                    return 
                        "SELECT a.username AS 'Brugernavn', h.type AS 'Type', CONCAT(a.first_names, ' ', a.last_name) AS 'Fuldt navn', ha.start_contract AS 'Kontraktdato', h.m2 AS 'M2', h.rental_price AS 'Husleje', CONCAT(h.street_address, ', ', h.locality_postal_code, ' ', l.city) AS 'Adresse' " +
                        "\nFROM housing_account ha, housing h, account a, locality l " +
                        "\nWHERE ha.account_username = a.username AND ha.housing_id = h.id AND l.postal_code = h.locality_postal_code " +
                        "\nORDER BY a.username;";
                
                case SELECTSQLQUERY.CurrentResidentsUsername:
                    return 
                        "SELECT account_username AS 'Brugernavn' " +
                        "\nFROM housing_account;";

                case SELECTSQLQUERY.WMSORTALL:
                    return 
                        "SELECT r.id AS 'Vaskemaskine' " +
                        "\nFROM resource r " +
                        "\nWHERE r.type = 'washingmachine';";

                case SELECTSQLQUERY.PHSortAll:
                    return 
                        "SELECT r.id AS 'Festsal' " +
                        "\nFROM resource r " +
                        "\nWHERE r.type = 'partyhall';";

                case SELECTSQLQUERY.PSSortAll:
                    return 
                        "SELECT r.id AS 'Parkeringsplads' " +
                        "\nFROM resource r " +
                        "\nWHERE r.type = 'parkingspace';";

                case SELECTSQLQUERY.AllResourcesBooked:
                    return 
                        "SELECT arr.id AS 'Booking id', a.username AS 'Brugernavn', CONCAT(a.first_names, ' ', a.last_name) AS 'Fuldt navn', r.`type` AS 'Type', r.id AS 'Type-id', arr.start_timestamp AS 'Starttidspunkt', arr.end_timestamp AS 'Sluttidspunkt' " +
                        "\nFROM account_resource_reservations arr, account a, resource r " +
                        "\nWHERE arr.account_username = a.username AND arr.resource_id = r.id AND NOW() < arr.end_timestamp " +
                        "\nORDER BY arr.end_timestamp;";

                case SELECTSQLQUERY.AvailableResourceIDS:
                    return 
                        "SELECT r.id AS 'Type-id' " +
                        "\nFROM account_resource_reservations arr, resource r " +
                        "\nWHERE r.`type` = @availabletype AND (r.id = arr.resource_id AND (@start > arr.end_timestamp OR @durationendtime < arr.start_timestamp) OR (r.id NOT IN(SELECT arr2.resource_id FROM account_resource_reservations arr2)))" +
                        "\nGROUP BY r.id ORDER BY r.id;";

                case SELECTSQLQUERY.AvailableResourcesByType:
                    return 
                        "SELECT r.id AS 'Type-id'" +
                        "\nFROM account_resource_reservations arr, resource r" +
                        "\nWHERE r.`type` = @availabletype AND (r.id = arr.resource_id AND (@start > arr.end_timestamp OR @durationendtime < arr.start_timestamp) OR (r.id NOT IN(SELECT arr2.resource_id FROM account_resource_reservations arr2)))" +
                        "\nGROUP BY r.id ORDER BY r.id;";

                case SELECTSQLQUERY.Usernames:
                    return 
                        "SELECT a.username AS 'Brugernavn' " +
                        "\nFROM account a " +
                        "\nWHERE a.privilege = 'resident' " +
                        "\nORDER BY a.username;";

                case SELECTSQLQUERY.StartDate:
                    return 
                        "SELECT DISTINCT arr.start_timestamp AS 'Starttidspunkt' " +
                        "\nFROM account_resource_reservations arr " +
                        "\nORDER BY arr.start_timestamp;";

                case SELECTSQLQUERY.EndDate:
                    return 
                        "SELECT DISTINCT rrr.end_timestamp AS 'Sluttidspunkt' " +
                        "\nFROM account_resource_reservations rrr " +
                        "\nORDER BY rrr.end_timestamp;";

                case SELECTSQLQUERY.BookingCancelIDs:
                    return 
                        "SELECT arr.id AS 'Booking id' " +
                        "FROM account_resource_reservations arr " +
                        "WHERE NOW() < arr.end_timestamp " +
                        "ORDER BY arr.end_timestamp;";

                case SELECTSQLQUERY.CurrentResidentInfo:
                    return
                        "SELECT hr.*, h.`type`, h.m2, h.rental_price, a.`type`, CONCAT(h.street_address, ', ', h.locality_postal_code, ' ', l.city) AS 'Adresse'" +
                        "\nFROM housing_account hr, housing h, account a, locality l" +
                        "\nWHERE h.id = hr.housing_id AND hr.account_username = a.username AND a.username = @username AND h.locality_postal_code = l.postal_code" +
                        "\nGROUP BY hr.housing_id;";

                case SELECTSQLQUERY.ResourceSortAllUsers:
                    return
                        "SELECT a.username, CONCAT(a.first_names, ' ', a.last_name) AS 'Fuldt navn', r2.type, r2.id, rrr.start_timestamp, rrr.end_timestamp " +
                        "\nFROM account_resource_reservations rrr, account r, account a, resource r2 " +
                        "\nWHERE rrr.account_username  = r.username AND rrr.resource_id = r2.id AND r.username = a.username " +
                        "\nORDER BY rrr.end_timestamp DESC ;";

                case SELECTSQLQUERY.ResourceSortAllPerUnit:
                    return 
                        "SELECT * " +
                        "\nFROM resource r " +
                        "\nWHERE r.`type` = @availabletype;";

                case SELECTSQLQUERY.ResourceSortPerUser:
                    return
                        "SELECT a.username, CONCAT(a.first_names, ' ', a.last_name) AS 'Fuldt navn', r.type, r.id, rrr.start_timestamp, rrr.end_timestamp " +
                        "\nFROM account_resource_reservations rrr, account a, resource r " +
                        "\nWHERE rrr.account_username = a.username AND rrr.resource_id = r.id AND rrr.start_timestamp >= @start AND rrr.end_timestamp <= @end AND rrr.account_username = @sortusername AND r.type = @availabletype" +
                        "\nORDER BY rrr.end_timestamp;";

                case SELECTSQLQUERY.ResourceSortPerUnit:
                    return 
                        "SELECT * " +
                        "\nFROM resource " +
                        "\nWHERE id = @unitid;";

                case SELECTSQLQUERY.AvailableHouseSortAll:
                    return
                        "SELECT h.id, h.`type`, h.rental_price, h.m2, CONCAT(h.street_address, ', ', h.locality_postal_code, ' ', l.city) AS 'Adresse'" +
                        "\nFROM housing h, locality l" +
                        "\nWHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_account hr2) AND h.locality_postal_code = l.postal_code" +
                        "\nGROUP BY h.id ORDER BY h.id;";

                case SELECTSQLQUERY.AvailableHouseSortByM2:
                    return
                        "SELECT h.id, h.`type`, h.rental_price, h.m2, CONCAT(h.street_address, ', ', h.locality_postal_code, ' ', l.city) AS 'Adresse'" +
                        "\nFROM housing h, locality l" +
                        "\nWHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_account hr2) AND h.m2 BETWEEN @min AND @max AND h.locality_postal_code = l.postal_code" +
                        "\nGROUP BY h.id ORDER BY h.id;";

                case SELECTSQLQUERY.AvailableHouseSortByPrice:
                    return
                        "SELECT h.id, h.`type`, h.rental_price, h.m2, CONCAT(h.street_address, ', ', h.locality_postal_code, ' ', l.city) AS 'Adresse'" +
                        "\nFROM housing h, locality l" +
                        "\nWHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_account hr2) AND h.rental_price BETWEEN @min AND @max AND h.locality_postal_code = l.postal_code" +
                        "\nGROUP BY h.id ORDER BY h.id;";

                case SELECTSQLQUERY.AvailableHouseIDs:
                    return 
                        "SELECT h.id " +
                        "\nFROM housing h " +
                        "\nWHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_account hr2) " +
                        "\nGROUP BY h.id ORDER BY h.id;";

                case SELECTSQLQUERY.UsernamesOnWaitinglist:
                    return
                        "SELECT a.username " +
                        "\nFROM account a " +
                        "\nWHERE privilege = 'waitlist';";

                case SELECTSQLQUERY.Zipcode:
                    return
                        "SELECT postal_code " +
                        "\nFROM locality;";

                default:
                    return "NONE";
            }
        }
    }
    #endregion SQLCMDS

    #region Open/Close-Conn
    private static string ConnStr = "server=62.61.157.3;port=80;database=2SemesterEksamen;user=plebs;password=1234;SslMode=none;";
    //Open Connection Method 
    public MySqlConnection OpenConn(MySqlConnection conn)
    {
        if (conn.State == ConnectionState.Closed)
        {
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        return conn;
    }

    public MySqlConnection CloseConn(MySqlConnection conn)
    {
        if (conn.State == ConnectionState.Open)
        {
            try
            {
                conn.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        return conn;
    }
    #endregion Open/Close-Conn

    #region Slogan Thread
    public async Task<string> SloganT()
    {
        try
        {
            List<string> list = new()
                { "Bo godt – bo hos Sønderbo", "test2", "test3", "test4", "test5", "test6", "test7" };

            Random r = new();
            int slogan = r.Next(0, list.Count);

            return await Task.FromResult(list[slogan]);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return await Task.FromResult("Slogan Error");
    }

    #endregion Slogan Thread

    #region Threading
    #region Database Update Information
    private async Task<DateTime> DBUpdateCheck()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);
            string connSql = "SELECT UPDATE_TIME FROM information_schema.tables WHERE TABLE_SCHEMA = '2SemesterEksamen' ORDER BY UPDATE_TIME DESC LIMIT 1;";
            MySqlCommand cmd = new(connSql, OpenConn(conn));
            MySqlDataReader reader = cmd.ExecuteReader();
            DateTime dbTime = DateTime.MaxValue;
            while (reader.Read())
            {
                try
                {
                    string? timeString = Convert.ToString(reader[0]);
                    if (timeString == string.Empty)
                    {
                        timeString = Convert.ToString(DateTime.MinValue, CultureInfo.InvariantCulture);
                    }
                    if (timeString != null || timeString != string.Empty || timeString != "NULL" || timeString != "[NULL]")
                    {
                        string[] dateFormats = { "dd/MM/yyyy HH.mm.ss", "M/d/yyyy H:mm:ss tt", "M/d/yyyy HH:mm:ss tt", 
                                                "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd.MM.yyyy", "dd-MM-yyyy", 
                                                "dd/MM/yyyy", "ddMMyyyy", "yyyy.MM.dd", "yyyy-MM-dd", 
                                                "yyyy/MM/dd", "yyyyMMdd" };
                        dbTime = Convert.ToDateTime(DateTime.ParseExact(timeString!, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToString("dd-MM-yyyy HH:mm:ss"));
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            reader.Close();
            CloseConn(conn);
            return await Task.FromResult(dbTime);
        }
        catch (MySqlException ex)
        {
            throw new(ex.ToString());
        }
    }
    #endregion Database Update Information
    #region GridviewFill
    public void Gridview(DataGridView gv, string dataTableSql, bool bypass, DataGridViewAutoSizeColumnMode mode)
    {
        try
        {
            MySqlConnection conn = new(ConnStr);
            DataTable tbl = new();
            tbl.Clear();
            MySqlCommand cmd1 = new(dataTableSql, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@min", Min);
            cmd1.Parameters.AddWithValue("@max", Max);
            string start = Start.ToString("yyyy-MM-dd HH:mm:ss");
            cmd1.Parameters.AddWithValue("@start", start);
            string end = End.ToString("yyyy-MM-dd HH:mm:ss");
            cmd1.Parameters.AddWithValue("@end", end);
            cmd1.Parameters.AddWithValue("@unitid", UnitID);
            cmd1.Parameters.AddWithValue("@availabletype", AvailableType);
            cmd1.Parameters.AddWithValue("@username", AccountUsername);
            cmd1.Parameters.AddWithValue("@sortusername", SortUsername);
            cmd1.Parameters.AddWithValue("@durationendtime", Duration.ToString("yy-MM-dd HH:mm:ss.ffff"));
            tbl.Load(cmd1.ExecuteReader());

            CloseConn(conn);

            if (DBUpdateCheck().Result >= DateTime.Now.AddMilliseconds(-5000) || gv.DataSource == null || bypass)
            {
                if (gv.InvokeRequired)
                {
                    gv.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                    {
                        var source = new BindingSource(tbl, null);
                        gv.DataSource = source;
                        gv.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)mode;
                    });
                }
            }
        }
        catch (MySqlException ex)
        {
            throw new(ex.ToString());
        }
    }
    #endregion GridviewFill
    #region ComboBoxFill
    public void ComboBoxFill(ComboBox combo, string sql)
    {
        try
        {
            List<string> usernames = new();
            List<string> currentcomboelements = new();
            usernames.Clear();
            currentcomboelements.Clear();
            MySqlConnection conn = new(ConnStr);
            //Set Isolation Level
            string StartTransaction = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(StartTransaction, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            //Begin Transation
            string sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            //Append To List
            cmd1 = new(sql, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@min", Min);
            cmd1.Parameters.AddWithValue("@max", Max);
            cmd1.Parameters.AddWithValue("@start", Start.ToString("yy-MM-dd HH:mm:ss.ffff"));
            cmd1.Parameters.AddWithValue("@end", End.ToString("yy-MM-dd HH:mm:ss.ffff"));
            cmd1.Parameters.AddWithValue("@unitid", UnitID);
            cmd1.Parameters.AddWithValue("@availabletype", AvailableType);
            cmd1.Parameters.AddWithValue("@username", AccountUsername);
            cmd1.Parameters.AddWithValue("@durationendtime", Duration.ToString("yy-MM-dd HH:mm:ss.ffff"));
            MySqlDataReader rdr = cmd1.ExecuteReader();

            while (rdr.Read())
            {
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    usernames.Add(rdr.GetString(i));
                }
            }
            rdr.Close();
            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            CloseConn(conn);

            foreach (string items in combo.Items)
            {
                currentcomboelements.Add(items);
            }

            bool isEqual = currentcomboelements.OrderBy(e => e).SequenceEqual(usernames.OrderBy(e => e)); //Sort Both Lists Using Lambda

            if (isEqual == false)
            {
                if (combo.InvokeRequired)
                {
                    combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                    {
                        combo.Items.Clear();
                        foreach (string ele in usernames)
                        {
                            combo.Items.Add(ele);
                        }
                    });
                }
            }
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
        

    public void ComboBoxFillNoSqlInt(ComboBox combo, int amount)
    {
        try
        {
            List<int> countlist = new();
            countlist.Clear();

            for (int i = 1; i <= amount; i++)
            {
                countlist.Add(i);
            }

            if (combo.Items.Count != countlist.Count || DBUpdateCheck().Result > DateTime.Now.AddMilliseconds(-5000))
            {
                if (combo.InvokeRequired)
                {
                    combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                    {
                        combo.Items.Clear();
                        foreach (int i in countlist)
                        {
                            combo.Items.Add(i);
                        }
                    });
                }
            }
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion ComboBoxFill
    #region ButtonComboboxGroupboxInvoker
    public void ButtonInvoker(Button btn, bool btnEnableDisable)
    {
        if (btn.Enabled != btnEnableDisable)
        {
            if (btn.InvokeRequired)
            {
                btn.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                {
                    btn.Enabled = btnEnableDisable;
                });
            }
        }
    }
    public void ComboBoxInvoker(ComboBox combo, bool cbEnableDisable)
    {
        if (combo.Enabled != cbEnableDisable)
        {
            if (combo.InvokeRequired)
            {
                combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                {
                    combo.Enabled = cbEnableDisable;
                });
            }
        }
    }
    public void GroupBoxInvoker(GroupBox gb, bool gbEnableDisable)
    {
        if (gb.Enabled != gbEnableDisable)
        {
            if (gb.InvokeRequired)
            {
                gb.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                {
                    gb.Enabled = gbEnableDisable;
                });
            }
        }
    }
    #endregion ButtonComboBoxInvoker
    #region Enums
    public enum SetReaderField
    {
        Min = 1,
        Max,
        AccountUsername,
        NewAccountUsername,
        CreateAccountUsername,
        SortUsername,
        HouseID,
        AccountName,
        AccountSurname,
        AccountPhoneNumer,
        SpecialCollectionSql,
        //User,
        Start,
        End,
        Duration,
        UnitID,
        StatisticSQL,
        CancelBookingID,
        AvailableType,
        Password,
        WaitlistType,
        DeleteFromSystemUsername,
        HouseType,
        M2,
        Price,
        Address,
        Zipcode
    }
    public enum ResourceSort
    {
        AllUsers = 1,
        AllPerUnit,
        PerUser,
        PerUnit,
        Everything,
    }
    #endregion
    #region GroupBoxReader
    public void GroupboxReader(GroupBox gb, SetReaderField whichField = 0)
    {
        try
        {
            switch (whichField)
            {
                case SetReaderField.AvailableType:
                    if (gb.InvokeRequired)
                    {
                        gb.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            AvailableType = gb.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)?.Text;
                        });
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion GroupBoxReader
    #region ComboBoxReader
    public void ComboBoxReader(ComboBox combo, SetReaderField whichField = 0)
    {
        try
        {
            switch (whichField)
            {
                case SetReaderField.Start:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = DateTime.TryParse(combo.Text, out DateTime result);
                                if (success)
                                {
                                    Start = result;
                                }
                            });
                        }

                        break;
                    }
                case SetReaderField.End:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = DateTime.TryParse(combo.Text, out DateTime result);
                                if (success)
                                {
                                    End = result;
                                }
                            });
                        }

                        break;
                    }
                case SetReaderField.Duration:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = Int32.TryParse(combo.Text, out int result);
                                if (success)
                                {
                                    Duration = Start.AddHours(result);
                                }
                            });
                        }

                        break;
                    }
                case SetReaderField.AccountUsername:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                AccountUsername = combo.Text;
                            });
                        }

                        break;
                    }

                case SetReaderField.AccountSurname:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                AccountSurname = combo.Text;
                            });
                        }

                        break;
                    }

                case SetReaderField.AccountPhoneNumer:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                 AccountPhoneNumber = combo.Text;
                            });
                        }

                        break;
                    }

                case SetReaderField.SortUsername:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                SortUsername = combo.Text;
                            });
                        }

                        break;
                    }

                case SetReaderField.UnitID:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = int.TryParse(combo.Text, out int result);
                                UnitID = success ? result : 0;
                            });
                        }

                        break;
                    }
                case SetReaderField.CancelBookingID:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = int.TryParse(combo.Text, out int result);
                                CancelBookingID = success ? result : 0;
                            });
                        }

                        break;
                    }

                case SetReaderField.HouseType:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                HouseType = combo.Text;
                            });
                        }

                        break;
                    }

                case SetReaderField.M2:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = int.TryParse(combo.Text, out int result);
                                M2 = success ? result : 0;
                            });
                        }

                        break;
                    }

                case SetReaderField.Price:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                bool success = int.TryParse(combo.Text, out int result);
                                Price = success ? result : 0;
                            });
                        }
                        break;
                    }

                case SetReaderField.Address:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                Address = combo.Text;
                            });
                        }

                        break;
                    }

                case SetReaderField.Zipcode:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                Zipcode = combo.Text;
                            });
                        }

                        break;
                    }

                case SetReaderField.NewAccountUsername:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                NewAccountUsername = combo.Text;
                            });
                        }

                        break;
                    }
                case SetReaderField.Password:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                Password = combo.Text;
                            });
                        }
                        break;
                    }
                case SetReaderField.HouseID:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                HouseID = combo.Text;
                            });
                        }
                        break;
                    }
                case SetReaderField.AccountName:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                AccountName = combo.Text;
                            });
                        }
                        break;
                    }
                case SetReaderField.CreateAccountUsername:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                CreateAccountUsername = combo.Text;
                            });
                        }
                        break;
                    }
                case SetReaderField.WaitlistType:
                    {
                        if (combo.InvokeRequired)
                        {
                            combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                            {
                                WaitlistType = combo.Text;
                            });
                        }
                        break;
                    }
                case SetReaderField.DeleteFromSystemUsername:
                {
                    if (combo.InvokeRequired)
                    {
                        combo.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                        {
                            DeleteFromSystemUsername = combo.Text;
                        });
                    }
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion ComboBoxReader
    #region TextBoxReader
    public void TextboxReader(TextBox txtbox, SetReaderField whichField = 0)
    {
        switch (whichField)
        {
            case SetReaderField.Min:
            {
                if (txtbox.InvokeRequired)
                {
                    txtbox.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                    {
                        bool success = int.TryParse(txtbox.Text, out int result);
                        Min = success ? result : 0;
                    });
                }

                break;
            }
            case SetReaderField.Max:
            {
                if (txtbox.InvokeRequired)
                {
                    txtbox.Invoke((MethodInvoker)delegate //Invoking due to GUI Thread //Delegate ref pointing to adress
                    {
                        bool success = int.TryParse(txtbox.Text, out int result);
                        Max = success ? result : int.MaxValue;
                    });
                }

                break;
            }
        }
    }
    #endregion TextBoxReader
    #region Special Collection Method
    internal class Houses
    {
        public int ID { get; set; }
        public string? Housetype { get; set; }
        public int M2 { get; set; }
        public int Price { get; set; }
        public string? Address { get; set; }
    }

    public enum SPECIALCOLLECTION
    {
        SortByAll = 1,
        SortByM2,
        SortByPrice
    }

    public void SpecialCollectionList(DataGridView gv, SPECIALCOLLECTION specialcollection = 0)
    {
        List<Houses> collection = new();
        collection.Clear();

        MySqlConnection conn = new(ConnStr);

        //Set Isolation Level
        string sqlString = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
        MySqlCommand cmd1 = new(sqlString, OpenConn(conn));
        cmd1.ExecuteNonQuery();

        //Begin Transation
        sqlString = "START TRANSACTION;";
        cmd1 = new(sqlString, OpenConn(conn));
        cmd1.ExecuteNonQuery();

        string dosql = "NONE";
        switch (specialcollection)
        {
            case SPECIALCOLLECTION.SortByAll:
                dosql = _sqlCMDS.GetSQLQuery(SQLCMDS.SELECTSQLQUERY.AvailableHouseSortAll);
                break;

            case SPECIALCOLLECTION.SortByM2:
                dosql = dosql = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.AvailableHouseSortByM2);

                break;

            case SPECIALCOLLECTION.SortByPrice:
                dosql = dosql = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.AvailableHouseSortByPrice);
                break;
        }

        //Append To List
        cmd1 = new(dosql, OpenConn(conn));
        cmd1.Parameters.AddWithValue("@min", Min);
        cmd1.Parameters.AddWithValue("@max", Max);
        MySqlDataReader rdr = cmd1.ExecuteReader();

        while (rdr.Read())
        {
            Houses house = new()
            {
                ID = rdr.GetInt32(0),
                Housetype = rdr.GetString(1),
                M2 = rdr.GetInt32(3),
                Price = rdr.GetInt32(2),
                Address = rdr.GetString(4),
            };

            collection.Add(house);
        }
        rdr.Close();
        //COMMIT
        string commit = "COMMIT;";
        cmd1 = new(commit, OpenConn(conn));
        cmd1.ExecuteNonQuery();
        CloseConn(conn);

        var bindingList = new BindingList<Houses>(collection); //Raise an event if the underlying list changes 
        var source = new BindingSource(bindingList, null);
        gv.DataSource = source;
        gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }
    #endregion Special Collection Method
    #endregion Threading
    #region Login
    public async Task<string> Login(string username, string password)
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            Regex regex = new(@"^[a-zA-Z0-9]+$"); //Input Validation
            string connSql = "SELECT username, AES_DECRYPT(password, 'key'), privilege, type FROM account WHERE username = @username";
            MySqlCommand cmd = new(connSql, OpenConn(conn));

            if (regex.IsMatch(username)) //Input Validation Check
            {
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = cmd.ExecuteReader();

                string dbusername = "NONE";
                string dbpassword = "NONE";
                string dbprivilege = "NONE";
                string type = "NONE";
                while (reader.Read())
                {
                    dbusername = reader.GetString(0);
                    dbpassword = reader.GetString(1);
                    dbprivilege = reader.GetString(2);
                    type = reader.GetString(3);
                }
                reader.Close();

                CloseConn(conn);

                if (dbusername == username && dbpassword == password)
                {
                    AccountUsername = dbusername;
                    switch (dbprivilege)
                    {
                        case "secretary":
                            return await Task.FromResult("secretary");
                        case "admin":
                            return await Task.FromResult("admin");
                        case "resident":
                            switch (type)
                            {
                                case "youth":
                                    return await Task.FromResult("youth");
                                case "senior":
                                    return await Task.FromResult("senior");
                                case "normal":
                                    return await Task.FromResult("normal");
                            }
                            break;
                    }
                }
                else
                {
                    return await Task.FromResult("Username or Password Does Not Exist");
                }
            }
            else
            {
                return await Task.FromResult("Incorrect Username Format!\nOnly Accepts A-Z & 0-9");
            }
        }
        catch (Exception ex)
        {
            throw new(ex.ToString());
        }
        return await Task.FromResult("NONE");
    }
    #endregion
    #region GetPassword
    public async Task<string> GetPassword(string username)
    {
        try
        {
            MySqlConnection conn = new(ConnStr);
            Regex regex = new(@"^[a-zA-Z0-9]+$"); //Input Validation
            string connSql = "SELECT username, AES_DECRYPT(password, 'key'), privilege FROM account WHERE username = @username";
            MySqlCommand cmd = new(connSql, OpenConn(conn));

            if (regex.IsMatch(username)) //Input Validation Check
            {
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = cmd.ExecuteReader();

                string dbusername = "NONE";
                string dbpassword = "NONE";
                string dbprivilege = "NONE";
                while (reader.Read())
                {
                    dbusername = reader.GetString(0);
                    dbpassword = reader.GetString(1);
                    dbprivilege = reader.GetString(2);
                }
                reader.Close();

                CloseConn(conn);

                return await Task.FromResult($"Username: {dbusername}\n Password: {dbpassword}\n Privilege: {dbprivilege}");
            }

            return await Task.FromResult("Incorrect Username Format!\nOnly Accepts A-Z & 0-9");
        }
        catch (Exception ex)
        {
            throw new(ex.ToString());
        }
    }
    #endregion GetPassword
    #region CancelReservation
    public void CancelReservation()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            //Set Isolation Level
            string StartTransaction = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(StartTransaction, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Begin Transation
            string sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Delete Booking
            string insert = "DELETE FROM account_resource_reservations WHERE id = @id;";
            cmd1 = new(insert, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@id", CancelBookingID);
            cmd1.ExecuteNonQuery();

            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion
    #region Booking
    public void Booking()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            //Set Isolation Level
            string StartTransaction = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(StartTransaction, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Begin Transation
            string sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Insert Booking
            string insert = "INSERT INTO account_resource_reservations(account_username, resource_id, start_timestamp, end_timestamp) VALUES(@user, @unitid, @start, @duration);";
            cmd1 = new(insert, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@user", SortUsername);
            cmd1.Parameters.AddWithValue("@start", Convert.ToDateTime(Start));
            cmd1.Parameters.AddWithValue("@unitid", UnitID);
            cmd1.Parameters.AddWithValue("@duration", Duration);
            cmd1.ExecuteNonQuery();


            //Alter Booking Count
            string altercount = "UPDATE resource SET times_reserved = times_reserved + 1 WHERE id = @unitid; ";
            cmd1 = new(altercount, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@unitid", UnitID);

            cmd1.ExecuteNonQuery();

            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            CloseConn(conn);
                
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion Booking
    
    #region AdminMethods
    #region SecretaryMethods
    #region Secretary Print Resident (txt)
    public void SecretaryPrint()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);
            //Write To txt file
            string cmd_TxtPrint =
                "SELECT a.username, h.type, CONCAT(a.first_names, ' ', a.last_name), ha.start_contract, h.m2, h.rental_price " +
                "FROM housing_account ha, housing h, account a " +
                "WHERE ha.account_username = a.username " +
                "AND ha.housing_id = h.id " +
                "ORDER BY a.username;";
            MySqlCommand cmd1 = new(cmd_TxtPrint, OpenConn(conn));
            MySqlDataReader rdr = cmd1.ExecuteReader();
            Directory.CreateDirectory(@"..\..\..\txts");
            string filePath = @"..\..\..\txts\Residencies.txt";
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                StreamWriter writer = new(stream, Encoding.UTF8);

                while (rdr.Read())
                {
                    writer.WriteLine(
                        "{\n" +
                        $"\tUsername: {Convert.ToString(rdr[0])}\n" +
                        $"\tType: {Convert.ToString(rdr[1])}\n" +
                        $"\tName: {Convert.ToString(rdr[2])}\n" +
                        $"\tContract_Date: {Convert.ToString(rdr[3])}\n" +
                        $"\tM2: {Convert.ToString(rdr[4])}\n" +
                        $"\tRental_Price: {Convert.ToString(rdr[5])}\n" +
                        "}\n" +
                        "\n");
                }
                writer.Close();
            }
            rdr.Close();
            CloseConn(conn);
            MessageBox.Show($@"File Downloaded To: {filePath[9..]}");
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion
    #region Create User And Waitlist
    public void CreateUserWaitlist()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);
            //Set Isolation Level
            string StartTransaction = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(StartTransaction, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            //Begin Transation
            string sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            //Create User
            Regex regex = new(@"^[a-zA-Z0-9ÆØÅæøå]+$"); //Input Validation
            if (regex.IsMatch(CreateAccountUsername!) && regex.IsMatch(Password!))
            {
                string sqlcommand = "INSERT INTO account (username, password, privilege, type, phone_number, first_names, last_name) " +
                    "VALUES (@username, AES_ENCRYPT(@password, 'key'), 'waitlist', @type, @phone, @accountsurname, @accountname);";
                cmd1 = new(sqlcommand, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@username", CreateAccountUsername);
                cmd1.Parameters.AddWithValue("@password", Password);
                cmd1.Parameters.AddWithValue("@type", WaitlistType);
                cmd1.Parameters.AddWithValue("@accountname", AccountName);
                cmd1.Parameters.AddWithValue("@accountsurname", AccountSurname);
                cmd1.Parameters.AddWithValue("@phone", AccountPhoneNumber);
                cmd1.ExecuteNonQuery();
                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();
                CloseConn(conn);
            }
            else
            {
                MessageBox.Show(@"Incorrect Username Format! Only Accepts A-Z & 0-9");
            }
            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            if (ex.Number == 1062)
            {
                throw new("Username Already Exists!\nSet Another Username");
            }

            throw new(ex.ToString());
        }
    }
    #endregion
    #endregion SecretaryMethods
    #region Grant Housing
    public void GrantHousing()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            string type = String.Empty;
            string housetype = String.Empty;

            //Check User Type
            string sqlcommand = "SELECT a.`type` FROM account a WHERE a.username = @username;";
            MySqlCommand cmd1 = new(sqlcommand, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@username", AccountUsername);
            MySqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                type = reader.GetString(0);
            }
            reader.Close();

            //Check House Type
            sqlcommand = "SELECT h.`type` FROM housing h WHERE h.id = @id;";
            cmd1 = new(sqlcommand, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(HouseID));
            reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                housetype = reader.GetString(0);
            }
            reader.Close();
            if (type == housetype && !String.IsNullOrEmpty(AccountUsername))
            {
                //Set Isolation Level
                string sqlString = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                cmd1 = new(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                sqlString = "START TRANSACTION;";
                cmd1 = new(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Insert Into Housing_Residents
                sqlcommand = "INSERT INTO housing_account (housing_id, account_username, start_contract) VALUES (@id, @username, CURRENT_TIMESTAMP);";
                cmd1 = new(sqlcommand, OpenConn(conn)); 
                cmd1.Parameters.AddWithValue("@id", HouseID);
                cmd1.Parameters.AddWithValue("@username", AccountUsername);
                cmd1.ExecuteNonQuery();

                //Update Account Status
                sqlcommand = "UPDATE account SET privilege = 'resident' WHERE username = @username";
                cmd1 = new(sqlcommand, OpenConn(conn));
                //cmd1.Parameters.AddWithValue("@housetype", housetype);
                cmd1.Parameters.AddWithValue("@username", AccountUsername);
                cmd1.ExecuteNonQuery();

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                CloseConn(conn);
            }
            else
            {
                MessageBox.Show($@"House type: {housetype} is not available for Member type: {type}");
            }
            CloseConn(conn);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Indtast gyldig information!"); ;
        }
           
    }
    #endregion Grant Housing
    #region Create New Housing
    public void CreateNewHouse()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);
            if (HouseType != string.Empty && M2 != 0 && Price != 0)
            {
                //Set Isolation Level
                string sqlString = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                sqlString = "START TRANSACTION;";
                cmd1 = new(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Insert Into Residents
                string sqlcommand = "INSERT INTO housing (type, m2, rental_price, street_address, locality_postal_code) VALUES (@type, @m2, @price, @address, @zip);";
                cmd1 = new(sqlcommand, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@type", HouseType);
                cmd1.Parameters.AddWithValue("@m2", M2);
                cmd1.Parameters.AddWithValue("@price", Price);
                cmd1.Parameters.AddWithValue("@address", Address);
                cmd1.Parameters.AddWithValue("@zip", Zipcode);

                cmd1.ExecuteNonQuery();

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                CloseConn(conn);
            }
            else
            {
                MessageBox.Show(@"Fill Out All Information!");
            }
            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            if (ex.Number == 1062)
            {
                throw new("Username Already Exists!\nSet Another Username");
            }
            
            if (ex.Number == 1452)
            {
                MessageBox.Show(@"Ukorrekt PostNr!");
            }
        }
    }
    #endregion Create New Housing
    #region Delete Housing
    public void DeleteHouse()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            if (HouseID != string.Empty)
            {
                //Set Isolation Level
                string sqlString = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
                MySqlCommand cmd1 = new(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Begin Transation
                sqlString = "START TRANSACTION;";
                cmd1 = new(sqlString, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                //Insert Into Residents
                string sqlcommand = "DELETE FROM housing WHERE id = @id;";
                cmd1 = new(sqlcommand, OpenConn(conn));
                cmd1.Parameters.AddWithValue("@id", Convert.ToInt32(HouseID));

                cmd1.ExecuteNonQuery();

                //COMMIT
                string commit = "COMMIT;";
                cmd1 = new(commit, OpenConn(conn));
                cmd1.ExecuteNonQuery();

                CloseConn(conn);
            }
            else
            {
                MessageBox.Show(@"Select a house id!");
            }
            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            if (ex.Number == 1062)
            {
                throw new("Username Already Exists!\nSet Another Username");
            }

            throw new(ex.Message);
        }
    }
    #endregion Delete Housing
    #region Admin Statistics Print (txt)
    public void AdminStatisticsPrint(ResourceSort sql)
    {
        try
        {
            bool printeverything = false;
            string cmdTxtPrint = "NONE";
            string cmdTxtPrint2 = "NONE";

            switch (sql)
            {
                case ResourceSort.AllUsers:
                    cmdTxtPrint = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.ResourceSortAllUsers);
                    break;

                case ResourceSort.AllPerUnit:
                    cmdTxtPrint = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.ResourceSortAllPerUnit);
                    break;

                case ResourceSort.PerUser:
                    cmdTxtPrint = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.ResourceSortPerUser);
                    break;

                case ResourceSort.PerUnit:
                    cmdTxtPrint = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.ResourceSortPerUnit);
                    break;

                case ResourceSort.Everything:
                    cmdTxtPrint = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.ResourceSortAllUsers);
                    cmdTxtPrint2 = SQLCMDS.GetInstance().GetSQLQuery(SQLCMDS.SELECTSQLQUERY.ResourceSortAllPerUnit);
                    printeverything = true;
                    break;
            }

            MySqlConnection conn = new(ConnStr);

            //Write To txt file
            MySqlCommand cmd1 = new(cmdTxtPrint, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@min", Min);
            cmd1.Parameters.AddWithValue("@max", Max);
            string start = Start.ToString("yyyy-MM-dd HH:mm:ss");
            cmd1.Parameters.AddWithValue("@start", start);
            string end = End.ToString("yyyy-MM-dd HH:mm:ss");
            cmd1.Parameters.AddWithValue("@end", end);
            cmd1.Parameters.AddWithValue("@unitid", UnitID);
            cmd1.Parameters.AddWithValue("@availabletype", AvailableType);
            cmd1.Parameters.AddWithValue("@username", AccountUsername);
            cmd1.Parameters.AddWithValue("@sortusername", SortUsername);
            cmd1.Parameters.AddWithValue("@durationendtime", Duration.ToString("yy-MM-dd HH:mm:ss.ffff"));

            MySqlDataReader rdr = cmd1.ExecuteReader();

            Directory.CreateDirectory(@"..\..\..\txts");
            string filePath = @"..\..\..\txts\Resources.txt";
            var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
            StreamWriter writer = new(stream, Encoding.UTF8);
            while (rdr.Read())
            {
                switch (sql)
                {
                    case ResourceSort.AllUsers:
                    case ResourceSort.Everything:
                        writer.WriteLine(
                            $"Brugernavn:{Convert.ToString(rdr[0])};Navn:{Convert.ToString(rdr[1])};Type:{Convert.ToString(rdr[2])};ID:{Convert.ToString(rdr[3])};Start Dato:{Convert.ToString(rdr[4])};Slut Dato:{Convert.ToString(rdr[5])};");
                        break;

                    case ResourceSort.AllPerUnit:
                        writer.WriteLine(
                            $"ID:{Convert.ToString(rdr[0])};Type:{Convert.ToString(rdr[1])};Gange_Reserveret: {Convert.ToString(rdr[2])};\t\t");
                        break;

                    case ResourceSort.PerUser:
                        writer.WriteLine(
                            $"Brugernavn:{Convert.ToString(rdr[0])};Navn:{Convert.ToString(rdr[1])};Type: {Convert.ToString(rdr[2])};ID:{Convert.ToString(rdr[3])};Start Dato:{Convert.ToString(rdr[4])};Slut Dato:{Convert.ToString(rdr[5])};");
                        break;

                    case ResourceSort.PerUnit:
                        writer.WriteLine(
                            $"\nID:{Convert.ToString(rdr[0])};Type:{Convert.ToString(rdr[1])};Gange_Reserveret:{Convert.ToString(rdr[2])};");
                        break;
                }
            }
            rdr.Close();
            writer.Close();
            stream.Close();
            CloseConn(conn);

            if (printeverything)
            {
                //Write Count To Txt
                MySqlCommand cmdCount = new(cmdTxtPrint2, OpenConn(conn));
                cmdCount.Parameters.AddWithValue("@availabletype", AvailableType);
                MySqlDataReader rdrCount = cmdCount.ExecuteReader();
                using var streamCount = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true);
                StreamWriter writerCount = new(streamCount, Encoding.UTF8);
                writerCount.WriteLine();
                while (rdrCount.Read())
                {
                    writerCount.WriteLine(
                        $"ID:{Convert.ToString(rdrCount[0])};Type:{Convert.ToString(rdrCount[1])};Gange_Reserveret:{Convert.ToString(rdrCount[2])}");
                }
                rdrCount.Close();
                writerCount.Close();
                streamCount.Close();
            }
            CloseConn(conn);
            MessageBox.Show($@"File Downloaded To: {filePath[9..]}");
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }
    #endregion Admin Statistics Print (txt)
    #region Delete Waitlist/Account
    public void DeleteWaitlistAccount()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            //Set Isolation Level
            string sqlString = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Begin Transation
            sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Delete From Waitlist
            sqlString = "DELETE FROM account WHERE username = @accountusername;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@accountusername", AccountUsername);

            cmd1.ExecuteNonQuery();

            //Delete Account
            sqlString = "DELETE FROM account WHERE username = @accountusername;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@accountusername", AccountUsername);

            cmd1.ExecuteNonQuery();

            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion
    #region DeleteResident/Account
    public void DeleteResidentAccount()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            //Set Isolation Level
            string sqlString = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Begin Transation
            sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Delete From Resource Reservations
            sqlString = "DELETE FROM account_resource_reservations WHERE account_username = @accountusername;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@accountusername", DeleteFromSystemUsername);

            cmd1.ExecuteNonQuery();

            //Delete From Housing Residents
            sqlString = "DELETE FROM housing_account WHERE account_username = @accountusername;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@accountusername", DeleteFromSystemUsername);

            cmd1.ExecuteNonQuery();

            //Delete Account
            sqlString = "DELETE FROM account WHERE username = @accountusername;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@accountusername", DeleteFromSystemUsername);

            cmd1.ExecuteNonQuery();

            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion
    #endregion AdminMethods
    
    #region Resident
    #region UpdateUsername
    public void UpdateUsername()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            //Set Isolation Level
            string StartTransaction = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(StartTransaction, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Begin Transation
            string sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Update Username
            string altercount = "UPDATE account SET username = @newaccountusername WHERE username = @username; ";
            cmd1 = new(altercount, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@newaccountusername", NewAccountUsername);
            cmd1.Parameters.AddWithValue("@username", AccountUsername);

            cmd1.ExecuteNonQuery();

            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion
    #region UpdatePassword
    public void UpdatePassword()
    {
        try
        {
            MySqlConnection conn = new(ConnStr);

            //Set Isolation Level
            string StartTransaction = "\nSET TRANSACTION ISOLATION LEVEL SERIALIZABLE;";
            MySqlCommand cmd1 = new(StartTransaction, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Begin Transation
            string sqlString = "START TRANSACTION;";
            cmd1 = new(sqlString, OpenConn(conn));
            cmd1.ExecuteNonQuery();

            //Update Username
            string altercount = "UPDATE account SET password = AES_ENCRYPT(@newpassword, 'key') WHERE username = @username; ";
            cmd1 = new(altercount, OpenConn(conn));
            cmd1.Parameters.AddWithValue("@newpassword", Password);
            cmd1.Parameters.AddWithValue("@username", AccountUsername);

            cmd1.ExecuteNonQuery();

            //COMMIT
            string commit = "COMMIT;";
            cmd1 = new(commit, OpenConn(conn));
            cmd1.ExecuteNonQuery();
            CloseConn(conn);
        }
        catch (MySqlException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    #endregion UpdatePassword
    #endregion Resident

}