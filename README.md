# demo-wpf
kick start for WPF development

# Background
This project help me to get used to WPF development, for the one who is new to WPF, I put all the learning materials, POC samples and coding practices in this [project's WIKI](https://github.com/dolphinotaku/demo-wpf/wiki) as an easy reference.

To help to push me forward, I simulated a real case.

# Business Case
Scope described the simulated real case. There is a barbecue site managed by a Club House.
This is a front-end program that will display barbecue booking data retrieved through WebAPI, this display system serves three roles of the user.
- clubhouse admin staff
- clubhouse facility staff, cleaner
- event participants

and display three types of booking information
- Organization Booking
- e-Booking
- Walk-in Booking
The system will display booking information about the ticket number, the type of 

Organization Bookings for a large group of people, participants will be dropped off at the Club House by shuttle bus. The organizer or organization representative will get down the bus first. He/she will register at the front desk, the organizer must provide the number of participants, confirm the number of barbecue pits/ Iron Gill Plates, and cookers (the clubhouse only provides cookers for Iron Gill Plates), then complete the payment in the registration process. 
On the registration process is complete, the clubhouse admin staff will assign the Organization Booking to the Barbecue zone. 
The organizer will back to the shuttle bus and tell the participants where (zone) to go and reserved for the Organization barbecue. The shuttle bus will drop off all the passengers at car park.

e-Booking for those who booked and paid online, the participants arrived on their own and register at the front desk. On the registration process is complete, the clubhouse admin staff will assign the e-Booking to the Barbecue zone.

walk-in Booking for those who didn't book online, the participants arrive on their own and register for a walk-in booking at the front desk. On the registration process is complete, the clubhouse admin staff will assign the walk-in Booking to the Barbecue zone.

# Business Flow
Create booking/appointment > Registration on arrival > Display guest info on Screen > Play welcome announcement

## Registration process
1. The clubhouse admin staff marks the appointment status to check-in
1. Assign the participants belonging to the corresponding Barbecue zone, the booking data will display on the screen
1. The registration process is complete

## Broadcast the voice
1. The system will broadcast the welcome announcement with the Barbecue zone location
2. clubhouse facility staff will guide and lead the guest to the popper Barbecue zone, and cleaners will go to the Barbecue zone to clear and set up charcoal in pit and fire, if the guest wants to set up and fire by their self, the cleaners could be leave early

# Terminology and/or Vocabulary

## Barbecue Category
The barbecue facility categories in types 3:
- C(Client) - bbq pit with client-self-service
- S(Staff) - bbq Grill with cooker service
- B(Bus) - Bus Booking (allow the combination of pit and grill)
- W(Walk-in) - Walk-In Booking

## Clubhouse Facility Staff, Cleaner
The facility staff and cleaner should clean, prepare and set up a fire depending on the type of barbecue.
- For the B(bus) type, having the top priority, please clean, set up Iron Gill Plate(s) and assign cookers, set up pit(s), and fire according to the registration information.
- For the C(Client) type, please clean and set up the pit fire
- For the S(Staff) type, please clean and set up the Iron Grill Plate
- For the W(Walk-in), having the lower priority, please clean and set up the pit fire 

# Functionality Scope
5 area
- Top bar (DateTime, Organization Name)
- Bus booking area
- Left side(C/S area), Right side(walk-in area)

## Get Data
- get client setting, operation data(Operation Mode, Fire Alarm) from API
- get booking data from API

## Refresh Display
display booking information in pagination

## Play Voice
- play welcome announcement 
- play Ad-hoc voice

## Operation Mode On/Off
- if on, the above features will function
- if off, the above features will not function
- the fire alarm model will not affected by the operation mode

## Fire Alarm On/Off
when fire alarm is on, the EVACUATE warning message and EVACUATE guide will display cover on whole screen

# Dependence
## NuGet
- NuGet\Install-Package AutoMapper -Version 10.1.1
>
> // this version is for the .Net Framework, use the latest one for .Net Core
>
- NuGet\Install-Package Serilog -Version 3.0.1
- NuGet\Install-Package Serilog.Sinks.Debug -Version 2.0.0
- NuGet\Install-Package Serilog.Sinks.File -Version 5.0.0
- NuGet\Install-Package Unity -Version 5.11.10
- NuGet\Install-Package Microsoft.Bcl.AsyncInterfaces -Version 7.0.0
>
> Microsoft.Bcl.AsyncInterfaces for
> // use async in .net framework 4.7.2
> // otherwise, complie error: async streams is not available in 7.3
> // https://bartwullems.blogspot.com/2020/01/asynchronous-streams-using.html
>


## Other Projects
[Sample Human Voice Audio](https://github.com/exotel/ivr-audio-prompts)
