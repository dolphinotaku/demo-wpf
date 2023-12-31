# demo-wpf
kick start for WPF development

# Background
This project help me to get used to WPF development, for the one who is new to WPF, I put all the learning materials, POC samples and coding practices in this [project's WIKI](https://github.com/dolphinotaku/demo-wpf/wiki) as an easy reference.

To help to push me forward, I simulated a real case.

# Scope
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

## Registration process
On the registration process is complete, the booking data will display on the screen after the clubhouse admin staff assigned the participants belonging to the corresponding Barbecue zone.

## Broadcast the voice
On each new booking arrived on screen. The system will broadcast the voice to welcome the participants and where should go. At that time, clubhouse facility staff, and cleaners should go to the Barbecue zone to clear and set up for the next round of the event.

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
