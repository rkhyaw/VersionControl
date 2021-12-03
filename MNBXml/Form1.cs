using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MNBXml.Entities;
using MNBXml.MBNServiceReference;

namespace MNBXml
{
    public partial class Form1 : Form
    {
        BindingList<RateData> _rates;

        public Form1()
        {
            InitializeComponent();
            getRates();
            dataGridView1.DataSource = _rates;
        }

        private void getRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody req = new GetExchangeRatesRequestBody();
            req.currencyNames = "EUR";
            req.startDate = "2020-01-01";
            req.endDate = "2020-06-30";
            var response = mnbService.GetExchangeRates(req);
            var result = response.GetExchangeRatesResult;
        }
    }
}
