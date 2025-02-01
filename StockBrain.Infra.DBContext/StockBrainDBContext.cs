using Microsoft.EntityFrameworkCore;
using StockBrain.Domain.Models;

namespace StockBrain.Infra.DBContext;

public class StockBrainDBContext(DbContextOptions<StockBrainDBContext> options) : DbContext(options)
{

	public DbSet<Account>? Account { get; set; } = default;
	public DbSet<AssetDecisionFactor>? AssetDecisionFactor { get; set; } = default;
	public DbSet<AssetMovement>? AssetMovement { get; set; } = default;
	public DbSet<Asset>? Asset { get; set; } = default;
	public DbSet<BondIssuer>? BondIssuer { get; set; } = default;
	public DbSet<Segment>? Segment { get; set; } = default;
	public DbSet<Sector>? Sector { get; set; } = default;
	public DbSet<Portfolio>? Portfolio { get; set; } = default;
	public DbSet<PortfolioAsset>? PortfolioAsset { get; set; } = default;
	public DbSet<PortfolioAssetMovement>? PortfolioAssetMovement { get; set; } = default;
	public DbSet<PortfolioAssetBroker>? PortfolioAssetBroker { get; set; } = default;
	public DbSet<DecisionFactor>? DecisionFactor { get; set; } = default;
	public DbSet<Broker>? Broker { get; set; } = default;
	public DbSet<Bond>? Bond { get; set; } = default;
	public DbSet<BondMovement>? BondMovement { get; set; } = default;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.Entity<Account>(e =>
		{
			e.ToTable(nameof(Account));
			e.ConfigureBaseProperties();
			e.Property(e => e.Name);
		});

		builder.Entity<Asset>(e =>
		{
			e.ToTable(nameof(Asset));
			e.HasKey(a => a.GUID);

			e.Property(a => a.Name)
				.IsRequired()
				.HasMaxLength(200);

			e.Property(a => a.Ticker)
				.IsRequired()
				.HasMaxLength(50);

			e.Property(a => a.Description)
				.IsRequired();

			e.Property(a => a.PositiveNotes)
				.IsRequired();

			e.Property(a => a.NegativeNotes)
				.IsRequired();

			e.HasMany(a => a.Factors)
				.WithOne()
				.HasForeignKey("AssetId")
				.IsRequired();

			e.Property(a => a.LastPriceUpdate)
				.IsRequired()
				.HasConversion(
					v => v.ToDateTime(TimeOnly.MinValue),
					v => DateOnly.FromDateTime(v)
				);

			e.Property(a => a.ReviewExpired)
				.IsRequired();

			e.Property(a => a.Risk)
				.IsRequired();

			e.OwnsOne(a => a.LastReview, lr =>
			{
				lr.Property(l => l.Date)
					.HasColumnName("LastReviewDate")
					.IsRequired()
					.HasConversion(
						d => d.ToDateTime(TimeOnly.MinValue),
						d => DateOnly.FromDateTime(d)
					);
				lr.Property(l => l.Span)
					.HasColumnName("LastReviewSpan")
					.IsRequired()
					.HasConversion<long>(
						t => t.Ticks,
						ticks => new TimeSpan(ticks)
					);
			});


			e.OwnsOne(a => a.IPO, lr =>
			{
				lr.Property(l => l.Date)
					.HasColumnName("IPODate")
					.IsRequired()
					.HasConversion(
						d => d.ToDateTime(TimeOnly.MinValue),
						d => DateOnly.FromDateTime(d)
					);
				lr.Property(l => l.Span)
					.HasColumnName("IPOSpan")
					.IsRequired()
					.HasConversion<long>(
						t => t.Ticks,
						ticks => new TimeSpan(ticks)
					);
			});


			e.OwnsOne(a => a.Foundation, lr =>
			{
				lr.Property(l => l.Date)
					.HasColumnName("FoundationDate")
					.IsRequired()
					.HasConversion(
						d => d.ToDateTime(TimeOnly.MinValue),
						d => DateOnly.FromDateTime(d)
					);
				lr.Property(l => l.Span)
					.HasColumnName("FoundationSpan")
					.IsRequired()
					.HasConversion<long>(
						t => t.Ticks,
						ticks => new TimeSpan(ticks)
					);
			});

			e.Property(a => a.Type)
				.IsRequired()
				.HasConversion<int>();

			e.Property(a => a.Sector)
				.IsRequired()
				.HasConversion<int>();

			e.Property(a => a.Segment)
				.IsRequired()
				.HasConversion<int>();

			e.Property(a => a.MarketPrice);
		});


		builder.Entity<AssetDecisionFactor>(e =>
		{
			e.Property(e => e.AssetID);
			e.Property(e => e.Answer);
			e.HasOne<DecisionFactor>().WithOne().HasForeignKey("Factor");
		});
	}
}
