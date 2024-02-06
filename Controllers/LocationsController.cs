using LocationAvailabilityAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace LocationAvailabilityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocationsController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = hostEnvironment;
        }

        // GET: api/locations
        [HttpGet]
        public IActionResult GetLocations()
        {
            var allLocations = _context.Locations.ToList();

            return Ok(allLocations);
        }

        [HttpPost("PullCSV")]
        public async Task<Response> PullCSV(IFormFile csv)
        {
            try
            {
                IFormFile fileData = csv;

               

                if (fileData == null || fileData.Length == 0)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Please upload file",
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        data = new object()
                    };
                }
                var path = _webHostEnvironment.ContentRootPath + "/Resources/Document/";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);


                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileData.FileName);
                var filePath = Path.Combine(path, fileName);
                string Extension = Path.GetExtension(filePath);

                if (Extension == ".csv")
                {

                    using (FileStream output = System.IO.File.Create(filePath))
                    {
                        await csv.CopyToAsync(output);
                    }

                    string returnPath = _webHostEnvironment.ContentRootPath+ "/Resources/Document/" + fileName;


                    using (FileStream fileStream = new FileStream(returnPath, FileMode.Open))
                    {
                        DataTable csvLines = ReadCsvFromFileStream(fileStream);

                        var list = new List<Dictionary<string, object>>();
                        foreach (DataRow row in csvLines.Rows)
                        {
                            var rowData = new Dictionary<string, object>();

                            foreach (DataColumn column in csvLines.Columns)
                            {
                                rowData[column.ColumnName] = row[column];
                            }

                            list.Add(rowData);

                        }

                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Data Fetched",
                            StatusCode = (int)HttpStatusCode.OK,
                            data = list
                        };
                    }
                }
                 return new Response
                {
                    IsSuccess = false,
                    Message = "Please upload valid file",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    data = new object()
                };
            }
            catch(Exception ex)
            {
                 return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    data = new object()
                };
            }            
        }


        private DataTable ReadCsvFromFileStream(Stream stream)
        {
            List<string> lines = new List<string>();
            DataTable dt = new DataTable();
            using (StreamReader reader = new StreamReader(stream))
            {
                
                DataRow row;
                string line = reader.ReadLine();
                string[] value = line.Split(',');
                foreach (string dc in value)
                {
                    dt.Columns.Add(dc);
                }
                while (!reader.EndOfStream)
                {
                    value = SplitCSV(reader.ReadLine());
                    if (value.Length == dt.Columns.Count)
                    {
                        row = dt.NewRow();
                        row.ItemArray = value;
                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }

        static Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

        private static string[] SplitCSV(string input)
        {

            List<string> list = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }
    }
}
