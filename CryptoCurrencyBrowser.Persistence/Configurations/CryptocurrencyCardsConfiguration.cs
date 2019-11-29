using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoCurrencyBrowser.Persistence.Configurations
{
    public class CryptocurrencyCardsConfiguration : IEntityTypeConfiguration<CryptocurrencyCard>
    {
        public void Configure(EntityTypeBuilder<CryptocurrencyCard> builder)
        {
            builder.ToTable("View_CryptoCurrencyCards");
        }
    }
}
