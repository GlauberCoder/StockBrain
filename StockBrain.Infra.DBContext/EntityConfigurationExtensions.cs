using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockBrain.Domain.Models;

namespace StockBrain.Infra.DBContext;

public static class EntityConfigurationExtensions
{
	public static void ConfigureBaseProperties<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
	{
		builder.HasKey(e => e.ID);
		builder.Property(e => e.ID).UseIdentityColumn().ValueGeneratedOnAdd();
		builder.Property(e => e.GUID);
	}
}