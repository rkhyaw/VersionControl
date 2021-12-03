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
using System.Windows.Forms.DataVisualization.Charting;

namespace MNBXml
{
    public partial class Form1 : Form
    {
        BindingList<RateData> _rates = new BindingList<RateData>();
        BindingList<string> currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = currencies;
            RefreshData();
        }

        private string getCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            GetCurrenciesRequestBody req = new GetCurrenciesRequestBody();
            var resp = mnbService.GetCurrencies(req);
            return "teszt";
        }

        private void loadCurrencyxml(string xmlstring)
        {
            currencies.Clear();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlstring);
            foreach (XmlElement item in xml.DocumentElement)
            {
                string s = item.InnerText;
                currencies.Add(s);
            }
        }

        private void RefreshData()
        {
            if (comboBox1.SelectedItem == null) return;
            _rates.Clear();
            loadXML(getRates());
            chartKeszites();
            dataGridView1.DataSource = _rates;
        }

        private void chartKeszites()
        {
            chartRateData.DataSource = _rates;
            var sorozatok=chartRateData.Series[0];
            sorozatok.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            sorozatok.XValueMember = "Date";
            sorozatok.YValueMembers = "Value";

            var jelmagyarazat=chartRateData.Legends[0];
            jelmagyarazat.Enabled = false;

            var diagramterulet = chartRateData.ChartAreas[0];
            diagramterulet.AxisY.IsStartedFromZero = false;
            diagramterulet.AxisY.MajorGrid.Enabled = false;
            diagramterulet.AxisX.MajorGrid.Enabled = false;
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
                if (childElement != null)
                {
                    r.Currency = childElement.GetAttribute("curr");
                    decimal unit = decimal.Parse(childElement.GetAttribute("unit"));
                    r.Value = decimal.Parse(childElement.InnerText);
                    if (unit != 0)
                    {
                        r.Value = r.Value / unit;
                    }

                    _rates.Add(r);
                }
            }
        }

        private string getRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody req = new GetExchangeRatesRequestBody();
            req.currencyNames = comboBox1.SelectedItem.ToString();
            req.startDate = tolPicker.Value.ToString("yyyy-MM-dd");
            req.endDate = igPicker.Value.ToString("yyyy-MM-dd");
            var response = mnbService.GetExchangeRates(req);
            return response.GetExchangeRatesResult;
        }

        private void paramChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
