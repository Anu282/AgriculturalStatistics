using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using AgriculturalStatistics.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
//using System.Web.Extensions;
using Nancy.Json;
using System.Text.Json;

namespace AgriculturalStatistics.DataAccess
{
    public static class DBInitializer
    {
        static HttpClient httpClient;
        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://catalog.data.gov/dataset/quick-stats-agricultural-database
        static string BASE_URL = "http://quickstats.nass.usda.gov/api/api_GET/";
        static string API_KEY = "7887F66A-0938-3A5B-9B7D-8F4524BE5665"; //Add your API key here inside ""


        public static void Initialize(ApplicationDBContext context)
        {
            context.Database.EnsureCreated();

            //getGroups(context);
            //getSectors(context);
            //GetCommodities(context);
            //GetFruitsCommodities(context);
            //GetDairy(context);
            //GetVegetables(context);
        }

        public static void getGroups(ApplicationDBContext context)
        {
            if (context.Groups.Any())
            {
                return;
            }
            //https://quickstats.nass.usda.gov/api/get_param_values/?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&param=group_desc&format=JSON
            string uri = "http://quickstats.nass.usda.gov/api/" + "get_param_values/?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&param=group_desc";
            string responsebody = "";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(uri);
            HttpResponseMessage response = httpClient.GetAsync(uri).GetAwaiter().GetResult();
            //Group groups = null;
            if (response.IsSuccessStatusCode)
            {
                responsebody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!responsebody.Equals(""))
            {
                // JsonConvert is part of the NewtonSoft.Json Nuget package
                // groups = JsonConvert.DeserializeObject<Group>(responsebody);
                JObject parsedResponse = JObject.Parse(responsebody);
                JArray jarraybrands = (JArray)parsedResponse["group_desc"];
                foreach (string jsonbrand in jarraybrands)
                {
                    Group group = new Group
                    {
                        GroupName = jsonbrand

                    };
                    context.Groups.Add(group);
                    context.SaveChanges();
                }
            }
           

           
        }

        public static void getSectors(ApplicationDBContext context)
        {
            if (context.Sectors.Any())
            {
                return;
            }
            //https://quickstats.nass.usda.gov/api/get_param_values/?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&param=group_desc&format=JSON
            string uri = "http://quickstats.nass.usda.gov/api/" + "get_param_values/?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&param=sector_desc";
            string responsebody = "";
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(uri);
            HttpResponseMessage response = httpClient.GetAsync(uri).GetAwaiter().GetResult();
            Sector sector = null;
            if (response.IsSuccessStatusCode)
            {
                responsebody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            if (!responsebody.Equals(""))
            {
                // JsonConvert is part of the NewtonSoft.Json Nuget package
                // groups = JsonConvert.DeserializeObject<Group>(responsebody);
                JObject parsedResponse = JObject.Parse(responsebody);
                JArray jarraybrands = (JArray)parsedResponse["sector_desc"];
                foreach (string jsonbrand in jarraybrands)
                {
                    Sector sectors = new Sector
                    {
                        SectorName = jsonbrand

                    };
                    context.Sectors.Add(sectors);
                    context.SaveChanges();
                }
            }
            


        }
        public static void GetCommodities(ApplicationDBContext context)
        {
            if (context.Commodities.Any())
            {
                return;
            }
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string Fruits_API_PATH = BASE_URL + "/?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&source_desc=CENSUS&sector_desc=CROPS&group_desc=FIELD%20CROPS&statisticcat_desc=SALES&year__GE=2018&state_alpha=VA&unit_desc=$&format=JSON";
            string CommodityData = "";
            
            httpClient.BaseAddress = new Uri(Fruits_API_PATH);
           

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(Fruits_API_PATH)
                                                        .GetAwaiter().GetResult();
                


                if (response.IsSuccessStatusCode)
                {
                    CommodityData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!CommodityData.Equals(""))
                {
                     JObject parsedResponse = JObject.Parse(CommodityData);
                    JArray jarraybrands = (JArray)parsedResponse["data"];
                    foreach (JObject jsonbrand in jarraybrands)
                    {
                     
                        string groupname = ((string)jsonbrand["group_desc"]);
                        string sectorname = ((string)(string)jsonbrand["sector_desc"]);
                       Commodity fruits = new Commodity
                        {
                            CommodityName = (string)jsonbrand["commodity_desc"],
                            DataItem = (string)jsonbrand["short_desc"],
                            Year = (string)jsonbrand["year"],
                            Geography = (string)jsonbrand["state_name"],
                            CV = (double)jsonbrand["CV (%)"],
                            Value = (double)jsonbrand["Value"],
                            Group=context.Groups.Where(c=>c.GroupName== groupname).FirstOrDefault(),
                            Sector=context.Sectors.Where(c=>c.SectorName==sectorname).FirstOrDefault()
                           
                       };
                        context.Commodities.Add(fruits);
                        context.SaveChanges();

                    }
                }
            }



            catch (Exception e)
            {
                Console.WriteLine((e.Message));
            }
           
        }

        public static void GetFruitsCommodities(ApplicationDBContext context)
        {
            if (context.Commodities.Any())
            {
                return;
            }

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string Fruits_API_PATH = BASE_URL + "?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&source_desc=CENSUS&sector_desc=CROPS&group_desc=FRUIT%20%26%20TREE%20NUTS&statisticcat_desc=SALES&year__GE=2018&state_alpha=VA&unit_desc=$&format=JSON";
            string CommodityData = "";
            
            httpClient.BaseAddress = new Uri(Fruits_API_PATH);
           
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(Fruits_API_PATH)
                                                        .GetAwaiter().GetResult();
               


                if (response.IsSuccessStatusCode)
                {
                    CommodityData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!CommodityData.Equals(""))
                {
                    
                    JObject parsedResponse = JObject.Parse(CommodityData);
                    JArray jarraybrands = (JArray)parsedResponse["data"];
                    foreach (JObject jsonbrand in jarraybrands)
                    {
                        string groupname = ((string)jsonbrand["group_desc"]);
                        string sectorname = ((string)(string)jsonbrand["sector_desc"]);
                        Commodity fruits = new Commodity
                        {
                            CommodityName = (string)jsonbrand["commodity_desc"],
                            DataItem = (string)jsonbrand["short_desc"],
                            Year = (string)jsonbrand["year"],
                            Geography = (string)jsonbrand["state_name"],
                            CV = (double)jsonbrand["CV (%)"],
                            Value = (double)jsonbrand["Value"],
                            Group = context.Groups.Where(c => c.GroupName == groupname).FirstOrDefault(),
                            Sector = context.Sectors.Where(c => c.SectorName == sectorname).FirstOrDefault()
                           
                        };
                        context.Commodities.Add(fruits);
                        context.SaveChanges();


                        
                    }
                }
            }



            catch (Exception e)
            {
                Console.WriteLine((e.Message));
            }

        }

        public static void GetDairy(ApplicationDBContext context)
        {
            if (context.Commodities.Any())
            {
                return;
            }
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string Fruits_API_PATH = BASE_URL + "?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&source_desc=CENSUS&sector_desc=ANIMALS%20%26%20PRODUCTS&group_desc=DAIRY&statisticcat_desc=SALES&year__GE=2019&unit_desc=$&format=JSON";
            string CommodityData = "";
           
            httpClient.BaseAddress = new Uri(Fruits_API_PATH);
            

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(Fruits_API_PATH)
                                                        .GetAwaiter().GetResult();
               


                if (response.IsSuccessStatusCode)
                {
                    CommodityData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!CommodityData.Equals(""))
                {
                    JObject parsedResponse = JObject.Parse(CommodityData);
                    JArray jarraybrands = (JArray)parsedResponse["data"];
                    foreach (JObject jsonbrand in jarraybrands)
                    {
                       
                        string groupname = ((string)jsonbrand["group_desc"]);
                        string sectorname = ((string)(string)jsonbrand["sector_desc"]);
                        Commodity dairy = new Commodity
                        {
                            CommodityName = (string)jsonbrand["commodity_desc"],
                            DataItem = (string)jsonbrand["short_desc"],
                            Year = (string)jsonbrand["year"],
                            Geography = (string)jsonbrand["state_name"],
                            CV = (double)jsonbrand["CV (%)"],
                            Value = (double)jsonbrand["Value"],
                            Group = context.Groups.Where(c => c.GroupName == groupname).FirstOrDefault(),
                            Sector = context.Sectors.Where(c => c.SectorName == sectorname).FirstOrDefault()
                        };
                        context.Commodities.Add(dairy);
                        context.SaveChanges();
                    }
                }
            }



            catch (Exception e)
            {
                Console.WriteLine((e.Message));
            }

        }
        public static void GetVegetables(ApplicationDBContext context)
        {
            if (context.Commodities.Any())
            {
                return;
            }
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string Fruits_API_PATH = BASE_URL + "?key=7887F66A-0938-3A5B-9B7D-8F4524BE5665&source_desc=CENSUS&sector_desc=CROPS&group_desc=VEGETABLES&statisticcat_desc=SALES&year__GE=2019&state_alpha=VA&unit_desc=$&format=JSON";
            string CommodityData = "";
            httpClient.BaseAddress = new Uri(Fruits_API_PATH);
           

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(Fruits_API_PATH)
                                                        .GetAwaiter().GetResult();
               

                if (response.IsSuccessStatusCode)
                {
                    CommodityData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!CommodityData.Equals(""))
                {
                   
                    JObject parsedResponse = JObject.Parse(CommodityData);
                    JArray jarraybrands = (JArray)parsedResponse["data"];
                    foreach (JObject jsonbrand in jarraybrands)
                    {
                      

                        string groupname = ((string)jsonbrand["group_desc"]);
                        string sectorname = ((string)(string)jsonbrand["sector_desc"]);
                        Commodity dairy = new Commodity
                        {
                            CommodityName = (string)jsonbrand["commodity_desc"],
                            DataItem = (string)jsonbrand["short_desc"],
                            Year = (string)jsonbrand["year"],
                            Geography = (string)jsonbrand["state_name"],
                            CV = (double)jsonbrand["CV (%)"],
                            Value = (double)jsonbrand["Value"],
                            Group = context.Groups.Where(c => c.GroupName == groupname).FirstOrDefault(),
                            Sector = context.Sectors.Where(c => c.SectorName == sectorname).FirstOrDefault()
                           
                        };
                        context.Commodities.Add(dairy);
                        context.SaveChanges();

                    }
                }
            }



            catch (Exception e)
            {
                Console.WriteLine((e.Message));
            }

        }
    }
}
