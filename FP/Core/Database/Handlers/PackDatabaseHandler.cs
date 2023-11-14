using FP.Core.Api.ApiDto;
using FP.Core.Api.Controllers;
using FP.Core.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FP.Core.Database.Handlers
{
	public class PackDatabaseHandler
	{
		private readonly ILogger<UserController> _logger;
		private readonly FpDbContext _dbContext;
		public PackDatabaseHandler(FpDbContext dbContext, ILogger<UserController> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<Pack?> CreatePack(PackDto packDto, int userId)
		{
			_logger.LogInformation("Start to add pack in database end date: {endDate}", packDto.EndDate);

			string status = "Ok";
			Pack pack = new Pack()
			{
				UserId = userId,
				EndDate = packDto.EndDate,
				DealSum = packDto.DealSum,
				StartDate = DateTime.UtcNow
			};
			try
			{
				await _dbContext.Packs.AddAsync(pack);
				await _dbContext.SaveChangesAsync();
				_logger.LogInformation("Pack created");
				return pack;
			}
			catch (Exception ex)
			{
				status = "Server error";
				_logger.LogInformation(status, "Cannot create pack");
				return null;
			}

		}
		public async Task<Pack?> GetPackById(int id)
		{
			_logger.LogInformation("Search pack in database{}", id);

			string status = "Ok";
			try
			{
				var pack = await _dbContext.Packs.FindAsync(id);
				if (pack != null)
				{
					_logger.LogInformation(status);
					return pack;
				}
				else
				{
					status = "Not found";
					_logger.LogInformation("Eror {}", status);
					return null;
				}
			}
			catch (Exception ex)
			{
				status = "Server error";
				_logger.LogInformation(ex, "Cannot create pack {}", status);
				return null;
			}
		}
	}
}
