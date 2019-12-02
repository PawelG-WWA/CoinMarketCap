using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoCurrencyBrowser.Persistence.Configurations
{
    public class CryptocurrencyCardsConfiguration : IQueryTypeConfiguration<CryptocurrencyCard>
    {
        public void Configure(QueryTypeBuilder<CryptocurrencyCard> builder)
        {
            builder.ToView("View_CryptocurrencyCards");
        }
    }
}
