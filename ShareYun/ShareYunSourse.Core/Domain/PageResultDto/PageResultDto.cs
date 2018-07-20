using System;
using System.Collections.Generic;
using System.Text;

namespace ShareYunSourse.Core.Domain.PageResultDto
{
   public class PageResultDto<T>
    {
        public PageResultDto() { }
        public int  Total { get; set; }
        public IReadOnlyList<T> Rows
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;
        public PageResultDto(int totalCount,IReadOnlyList<T> items)
        {
            Rows = items;
            Total = totalCount;
        }
    }
}
