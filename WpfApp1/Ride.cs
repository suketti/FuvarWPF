using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Ride
    {
        int taxiId;
        DateTime startTime;
        int rideTime; //seconds
        float distance;
        float deliveryCost;
        float tip;
        string paymentInfo;

        public Ride(int taxiId, DateTime startTime, int rideTime, float distance, float deliveryCost, float tip, string paymentInfo)
        {
            this.taxiId = taxiId;
            this.startTime = startTime;
            this.rideTime = rideTime;
            this.distance = distance;
            this.deliveryCost = deliveryCost;
            this.tip = tip;
            this.paymentInfo = paymentInfo;
        }

        public int TaxiId { get => taxiId; set => taxiId = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }
        public int RideTime { get => rideTime; set => rideTime = value; }
        public float Distance { get => distance; set => distance = value; }
        public float DeliveryCost { get => deliveryCost; set => deliveryCost = value; }
        public float Tip { get => tip; set => tip = value; }
        public string PaymentInfo { get => paymentInfo; set => paymentInfo = value; }
    }
}
