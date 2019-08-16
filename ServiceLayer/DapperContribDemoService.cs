using Dapper.Contrib.Extensions;
using System;
using System.Data.SqlClient;

namespace ServiceLayer
{
    //TODO: to add tests to Dapper contrib
    //TODO: you may also refer to some of the other Dapper extension libraries such as Dapper.Rainbow or DapperExtensions

    public class DapperContribDemoService
    {
        string connectionString = "Data Source=(local);Initial Catalog=DapperExample;Integrated Security=SSPI";

        //Default it expects table name as plural of entity name ... else we need to specify explicity like below
        [Dapper.Contrib.Extensions.Table("Event")]
        public class Event
        {
            //add the “Key” attribute from our Dapper.Contrib library to tell it that’s our primary key.
            [Dapper.Contrib.Extensions.Key]
            public int Id { get; set; }
            public int EventLocationId { get; set; }
            public string EventName { get; set; }
            public DateTime EventDate { get; set; }
            public DateTime DateCreated { get; set; }
        }

        public void AddEvent(Event newEvent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Insert(newEvent);// we are using Dapper.Contrib.Extensions;
            }
        }

        public Event GetAnEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var eventResult = connection.Get<Event>(eventId);
                return eventResult;
            }
        }

        public void UpdateEvent(Event updatedEventInfo)
        {
            //Dapper.Contrib does have dirty tracking, but IMO that’s just as pointless 
            //as you still need to get the full object by ID in the first place to do entity tracking,
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var myEvent = connection.Get<Event>(updatedEventInfo.Id);
                myEvent.EventName = "New Name";
                connection.Update(myEvent);
            }
        }

        public void DeleteEvent(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Delete(new Event { Id = id });
            }
        }        
    }
}
