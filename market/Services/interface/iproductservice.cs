namespace market.Services.internalinterface
{
    public interface iproductservice<t>where t : class
    {
        Task<IEnumerable<t>> getallasync();
        Task<t?> getbyid(int id);
        Task<t> createasync(t entity);
        Task<bool>updateasync(int id,t entity);
        Task<bool> deleteasync(int id);

    }
}
