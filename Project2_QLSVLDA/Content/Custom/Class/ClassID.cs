using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2_QLSVLDA.Content.Custom.Class
{
    public class ClassID
    {
        public string Id { get; set; }
        // Các thuộc tính khác của model

        public List<object> FilterByNhom(List<object> ketqua, string nhom)
        {
            var filteredResult = ketqua.Where(item => item.GetType().GetProperty("nhom").GetValue(item).ToString() == nhom).ToList();
            return filteredResult;
        }
    }


}