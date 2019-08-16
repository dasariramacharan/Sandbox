using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ServiceLayer
{    //TODO: to add tests to Dapper usage
    public class DapperDemoService
    {
        //I’ve found myself using doing writes using a different ORM (EF or NHibernate), but sticking with Dapper for querying
        // there is an extension library for Dapper called Dapper.Contrib that actually makes this super simple

        string connectionString = "Data Source=(local);Initial Catalog=DapperExample;Integrated Security=SSPI";

        public string GetAnEventName()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var eventName = connection.QueryFirst<string>("SELECT EventName FROM Event WHERE Id = 1");
                return eventName;

            }
        }

        public Event GetAnEvent()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var eventEntity = connection.QueryFirst<Event>("SELECT * FROM Event WHERE Id = 1");
                return eventEntity;
            }
        }

        public class Event
        {
            public int Id { get; set; }
            public int EventLocationId { get; set; }
            public string EventName { get; set; }
            public DateTime EventDate { get; set; }
        }


        public class EventDto
        {
            public int Id { get; set; }
            public string EventName { get; set; }
        }

        public EventDto GetEventDto(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Dapper handles sql injection or us with proper parameterization
                var eventEntity = connection.QueryFirst<EventDto>("SELECT Id, EventName FROM Event WHERE Id = @Id", new { Id = eventId });
                //or var eventEntity = connection.QueryFirst<EventDto>($"SELECT Id, EventName FROM Event WHERE Id = {eventId}");
                return eventEntity;
            }
        }

        public IEnumerable<EventDto> GetAllEvents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var allEvents = connection.Query<EventDto>("SELECT Id, EventName FROM Event");
                return allEvents;
            }
        }

        public void UpdateEvent(EventDto eventdto)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("UPDATE Event SET EventName = @EventName WHERE Id = @EventId", new { EventName = eventdto.EventName, EventId = eventdto.Id });
            }
        }

        public void AddEvent(EventDto eventdto, DateTime dateTime)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute("INSERT INTO Event (EventLocationId, EventName, EventDate, DateCreated) VALUES(@EventId, @EventName, @EventDate ,GETUTCDATE())",
                    new { EventName = eventdto.EventName, EventId = eventdto.Id, EventDate = dateTime });
            }
        }
        public void DeleteEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute($"DELETE FROM Event WHERE Id = {eventId}");
            }

        }
    }
}
