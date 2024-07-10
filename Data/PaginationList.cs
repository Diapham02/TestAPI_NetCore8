namespace Test_API.Data
{
    public class PaginationList
    {
        public int _totalPage { get; }
        public int _pageNumber { get; }
        public List<SinhVien> _paginationList { get; } // Kiểu List của paginationList

        //Contructor nhận vào total, number và list để khởi tạo obj PaginationList
        public PaginationList(int TotalPage, int PageNumber, List<SinhVien> PaginationLists)
        {
            _totalPage = TotalPage;
            _pageNumber = PageNumber;
            _paginationList = PaginationLists;
        }
    }
}