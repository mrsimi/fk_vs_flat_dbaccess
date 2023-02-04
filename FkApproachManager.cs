using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using flat_vs_fk.Models;
using Microsoft.EntityFrameworkCore;

namespace flat_vs_fk
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FkApproachManager
    {
        FkApproachDbContext _dbContext; 

        [GlobalSetup]
        public void GlobalSetup()
        {
            _dbContext = new FkApproachDbContext();
        }

        
        [Benchmark]
        
        public dynamic GetTracks()
        {
            var result = _dbContext.Track
                        .Include(m => m.MediaType)
                        .Include(m => m.Genre)
                        .Include(m => m.Album)
                        .ThenInclude(m => m.Artist)
                        .Select(m => new {
                            m.Name, 
                            m.Composer, 
                            m.UnitPrice,
                            m.Album.Title,
                            Artist= m.Album.Artist.Name,
                            MediaType = m.MediaType.Name,
                            Genre = m.Genre.Name
                        }).ToList();

            return result;    
        }

        [Benchmark]
        public Track AddTrack()
        {
            var track = new Track
            {
                Name = "Holy Ground",
                Composer = "David Adeleke",
                Milliseconds = 1000,
                Bytes = 20,
                UnitPrice = 1.5m, 
                MediaTypeId = 1, 
                AlbumId = 1, 
                GenreId = 1
            };

            _dbContext.Track.Add(track);
            _dbContext.SaveChanges();

            return track;
        }
    }
}