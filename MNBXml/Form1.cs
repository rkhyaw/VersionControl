using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MNBXml.Entities;
using MNBXml.MBNServiceReference;

namespace MNBXml
{
    public partial class Form1 : Form
    {
        BindingList<RateData> _rates = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();            
            loadXML(getRates());
            dataGridView1.DataSource = _rates;
        }

        private void loadXML(string xmlstring)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);
            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData r = new RateData();
                r.Date=DateTime.Parse(item.GetAttribute("date"));
                var childElement = (XmlElement)item.ChildNodes[0];
                r.Currency = childElement.GetAttribute("curr");
                decimal unit = decimal.Parse(childElement.GetAttribute("unit"));
                r.Value = decimal.Parse(childElement.InnerText);
                if(unit!=0)
                {
                    r.Value = r.Value / unit;
                }
                _rates.Add(r);
            }
        }

        private string getRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody req = new GetExchangeRatesRequestBody();
            req.currencyNames = "EUR";
            req.startDate = "2020-01-01";
            req.endDate = "2020-06-30";
            var response = mnbService.GetExchangeRates(req);
            return response.GetExchangeRatesResult;
        }
    }
}
