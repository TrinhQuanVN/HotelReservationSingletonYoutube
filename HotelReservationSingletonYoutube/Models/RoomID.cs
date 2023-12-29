using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSingletonYoutube.Models
{
    public class RoomID
    {
        public int RoomNumber { get; }
        public int FloorNumber { get; }

        public RoomID(int roomNumber, int floorNumber)
        {
            RoomNumber = roomNumber;
            FloorNumber = floorNumber;
        }
        public override string ToString()
        {
            return $"{RoomNumber}{FloorNumber}"; 
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(RoomNumber,FloorNumber);
        }
        public override bool Equals(object? obj)
        {
            return obj is RoomID roomID && RoomNumber == roomID.RoomNumber && FloorNumber == roomID.FloorNumber ;
        }
        public static bool operator ==(RoomID roomID1, RoomID roomID2)
        {
            if (roomID1 is null && roomID2 is null)
            {
                return true;
            }
            return !(roomID1 is null) && roomID1.Equals(roomID2);
        }
        public static bool operator !=(RoomID roomID1, RoomID roomID2)
        {
            return !(roomID1 == roomID2);
        }
    }
}
