

namespace LS.DAL.ViewModels
{
    public class VMSpGetLoginHistories
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? DateTime{ get; set; }
        public string IpAddress{ get; set; }
        public string? Browser{ get; set; }
        public string? OS { get; set; }

        public string? Device { get; set; }
    }

    public class VMGetLoginHistories
    {
        public int Count { get; set; }

        public List<VMSpGetLoginHistories>? LoginHistories { get; set; }
    }
}
