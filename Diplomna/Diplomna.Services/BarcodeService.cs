﻿using Diplomna.Common;
using Diplomna.Common.Auth;
using Diplomna.Common.Dtos;
using Diplomna.Models;
using Diplomna.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diplomna.Services
{
    public class BarcodeService : IBarcodeService
    {
        private DiplomnaContext _context;
        private AuthConstants _authConstants;

        public BarcodeService(DiplomnaContext context, AuthConstants authConstants)
        {
            _context = context;
            _authConstants = authConstants;
        }

        public async Task<Result<bool>> LogScanInfo(BarcodeDto input)
        {
            var authResult = Auth.DecodeToken(input.Token, _authConstants.PublicKey);
            if (authResult is null || DateTime.Parse(authResult.Exp) < DateTime.UtcNow)
            {
                return Result<bool>.BadResult("Expired session");
            }

            var user = await _context.Users.FirstOrDefaultAsync(p => p.Email == authResult.Email);
            if (user is null)
            {
                return Result<bool>.BadResult("Invalid token data");
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(p => p.RoomNumber == int.Parse(input.ScanInfo));
            if (room is null)
            {
                return Result<bool>.BadResult("Invalid qr code scanned");
            }

            await _context.Attendances.AddAsync(new Attendance()
            {
                PresenceConfirmed = false,
                RoomId = room.Id,
                TimeScanned = DateTime.Now,
                UserId = user.Id,
            });

            await _context.SaveChangesAsync();
            return Result<bool>.OkResult(true);
        }
    }
}
