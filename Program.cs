using System;
using System.Linq;

namespace Hotel_Reservations
{
    class Program
    {
        private static readonly string Decline = "Decline";
        private static readonly string Accept = "Accept";

        static void Main(string[] args)
        {
            Console.WriteLine("Hotel Reservations! \n");

            //Input Test Cases 

            var bookings1a = new[]{
                new Booking{
                    StartDate = -4, EndDate = 2
                }
            };
            var bookings1b = new[]{
                new Booking{
                    StartDate = 200, EndDate = 400
                }
            };

            var bookings2 = new[]{
                new Booking{
                    StartDate = 0, EndDate = 5
                },
                new Booking{
                    StartDate = 7, EndDate = 13
                },
                new Booking{
                    StartDate = 3, EndDate = 9
                },
                new Booking{
                    StartDate = 5, EndDate = 7
                },
                new Booking{
                    StartDate = 6, EndDate = 6
                },
                new Booking{
                    StartDate = 0, EndDate = 4
                }
            };

            var bookings3 = new[]{
                new Booking{
                    StartDate = 1, EndDate = 3
                },
                new Booking{
                    StartDate = 2, EndDate = 5
                },
                new Booking{
                    StartDate = 1, EndDate = 9
                },
                new Booking{
                    StartDate = 0, EndDate = 15
                }
            };

            var bookings4 = new[]{
                new Booking{
                    StartDate = 1, EndDate = 3
                },
                new Booking{
                    StartDate = 0, EndDate = 15
                },
                new Booking{
                    StartDate = 1, EndDate = 9
                },
                new Booking{
                    StartDate = 2, EndDate = 5
                },
                new Booking{
                    StartDate = 4, EndDate = 9
                }
            };

            var bookings5 = new[]{
                new Booking{
                    StartDate = 1, EndDate = 3
                },
                new Booking{
                    StartDate = 0, EndDate = 4
                },
                new Booking{
                    StartDate = 2, EndDate = 3
                },
                new Booking{
                    StartDate = 5, EndDate = 5
                },
                new Booking{
                    StartDate = 4, EndDate = 10
                },
                new Booking{
                    StartDate = 10, EndDate = 10
                },
                new Booking{
                    StartDate = 6, EndDate = 7
                },
                new Booking{
                    StartDate = 8, EndDate = 10
                },
                new Booking{
                    StartDate = 8, EndDate = 9
                }
            };

            object[] roomCounts = { 1, 1, 3, 3, 3, 2 };
            object[] bookings = { bookings1a, bookings1b, bookings2, bookings3, bookings4, bookings5 };

            //Executing Test Cases 

            for (int s = 0; s < bookings.Count(); s++)
            {
                Console.WriteLine($"TestCase {s}  HotelSize { (int)roomCounts[s] }");
                ReserveHotel((Booking[])bookings[s], (int)roomCounts[s]);
                Console.WriteLine("\n");
            }
        }

        private static void ReserveHotel(Booking[] bookings, int RoomCount)
        {
            // Validate Current Booking Date for Correctness and then Proceed for Reserving rooms

            var maxDays = bookings.Max(x => x.EndDate);
            int[,] HotelArr = new int[RoomCount, maxDays+1];

            foreach (Booking bk1 in bookings)
            {
                if (!ValidateDays(bk1))
                {
                    Console.WriteLine(Decline);
                    continue;
                }

                CheckBooking(HotelArr, bk1, RoomCount);
            }
        }

        private static void CheckBooking(int[,] hotelArr, Booking bk, int HotelSize)
        {

            for (int room = 0; room < HotelSize;)
            {
                //Check if booking Available in hotel with given HotelSize for current booking bk
                //If Available Book with Current Room or repeat process with other rooms

                if (CheckAvailability(hotelArr, bk, room))
                {
                    ReserveRoom(hotelArr, bk, room);
                    Console.WriteLine(Accept);
                    break;
                }
                else
                {
                    room++;
                    if (room == HotelSize)
                    {
                        Console.WriteLine(Decline);
                        break;
                    }
                }
            }
        }

        private static void ReserveRoom(int[,] hotelArr, Booking bk, int room)
        {
            //Reserve Room in Hotel from Start Date To End Date for Current Booking

            for (int day= bk.StartDate; day<= bk.EndDate; day++)
            {
                hotelArr[room,day] = 1;
            }
        }

        private static bool CheckAvailability(int[,] hotelArr, Booking bk, int room)
        {
            //Check If Room is available in Hotel from Start Date To End Date for Current Booking

            int j = 0;
            for (int day = bk.StartDate; day <= bk.EndDate; day++)
            {
                if (hotelArr[room, day] == 0)
                {
                    j++;
                }
            }

            if (bk.EndDate - bk.StartDate + 1 == j)
                return true;
            else
                return false;
        }
        private static bool ValidateDays(Booking booking)
        {
            //Validate current booking date range for correctness

            if (booking.StartDate > booking.EndDate)
                return false;
            if (booking.StartDate < 0 || booking.EndDate < 0 || booking.StartDate > 365 || booking.EndDate > 365)
                return false;
            else
                return true;
        }
    }
    public class Booking
    {
        public int StartDate;
        public int EndDate;
    }
}
