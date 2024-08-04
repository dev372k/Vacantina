using MongoDB.Bson.Serialization.Attributes;
using Shared.Commons;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Documents;

internal class Booking
{
    public Booking(string data, enBookingType bookingType) =>
       (Data, BookingType) = (data, bookingType);

    [BsonElement("data")]
    public string Data { get; }

    [BsonElement("bookinType")]
    public enBookingType BookingType { get; }
}
