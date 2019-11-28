using CryptoCurrencyBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoCurrencyBrowser.Persistence.Configurations
{
    public class CryptocurrencyConfiguration : IEntityTypeConfiguration<Cryptocurrency>
    {
        public void Configure(EntityTypeBuilder<Cryptocurrency> builder)
        {
            builder.HasKey(cryptocurrency => new { cryptocurrency.Id, cryptocurrency.ExternalId });
        }
    }
}
