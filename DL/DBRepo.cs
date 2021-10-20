﻿using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DL
{
    public class DBRepo : IRepo
    {
        private CacophanyDBContext _context;
        public DBRepo(CacophanyDBContext context)
        {
            _context = context;
        }

        //Playlist Methods//
        public async Task<Playlist> AddPlaylistAsync(Playlist playlist)
        {
            await _context.AddAsync(playlist);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return playlist;

        }
        public async Task<Playlist> GetPlaylistByIdAsync(int id)
        {
            return await _context.Playlists
                .AsNoTracking()
                //.Include(r => r.Reviews)
                .FirstOrDefaultAsync(r => r.PlaylistID == id);
        }

        public List<Playlist> GetPlaylistByUserId(int id)
        {
            return _context.Playlists.Where(p => p.UserID == id)
                .Select(i => new Playlist()).ToList();
                
        }


        //User Methods//
        public async Task<User> AddUserAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return user;

        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                //.Include(r => r.Reviews)
                .FirstOrDefaultAsync(r => r.UserID == id);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users
                .Select(
                user => new User()
                {
                    UserID = user.UserID,
                    Account = user.Account,
                    Email = user.Email
                }
             ).ToList();
        }
    }


    
}
