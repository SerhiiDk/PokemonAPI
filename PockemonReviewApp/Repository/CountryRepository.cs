namespace PockemonReviewApp.Repository
{
    public class CountryRepository :ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context )
        {
            _context = context;
        }

        public bool CountryExist(int id)
        {
            return _context.Countries.Any(x => x.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
            _context.Countries.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(o => o.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(x => x.Id == id).FirstOrDefault();   
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(s => s.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Countries.Update(country);
            return Save();
        }

      }
}
