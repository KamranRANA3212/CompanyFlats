﻿ALTER TABLE Booking
ADD CONSTRAINT FK_BookingFlat
FOREIGN KEY (Flat) REFERENCES Flat(Id);

ALTER TABLE Booking
ADD CONSTRAINT FK_BookingStatus
FOREIGN KEY (Status) REFERENCES LookUp(Id);


ALTER TABLE Booking
ADD CONSTRAINT FK_BookingRequest
FOREIGN KEY (BookingRequestId) REFERENCES BookingRequest(Id);

