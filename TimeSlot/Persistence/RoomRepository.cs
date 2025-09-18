using Microsoft.EntityFrameworkCore.Diagnostics;
using TimeSlot.Data;
using TimeSlot.Models;

namespace TimeSlot.Persistence
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TimeSlotContext _context;

        public RoomRepository(TimeSlotContext context)
        {
            _context = context;
        }

        public void Add(Room room)
        {
            if (room == null)
            {
                _context.Add(room);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            if (id != null)
            {
                var room = _context.rooms.Find(id);
                _context.Remove(room);
                _context.SaveChanges();
            }
           
        }

        public List<Room> GetAll()
        {
            return _context.rooms.ToList();
        }

        public Room? GetById(int id)
        {
            if (id != null)
            {
                var room = _context.rooms.Find(id);
                return (room);
            }
            return null;
            
        }

        public void Update(Room room)
        {
            if (room != null)
            {
                var rooms = _context.rooms.Find(room.RoomId);
                _context.Update(room);
                _context.SaveChanges();
            }

        }
    }
}
