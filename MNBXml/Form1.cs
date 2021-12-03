using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MNBXml.MBNServiceReference;

namespace MNBXml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getRates();
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
