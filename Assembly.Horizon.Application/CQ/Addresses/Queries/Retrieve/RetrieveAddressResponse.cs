﻿namespace Assembly.Horizon.Application.CQ.Addresses.Queries.Retrieve;

public class RetrieveAddressResponse
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Reference { get; set; }
    }
