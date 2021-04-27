/**
 * Created 25/11/2020
 * By: Sharek Khan
 * Last modified 25/11/2020 
 * By: Sharek Khan
 * 
 * Contributors: Aswad Mirza, Sharek Khan
 */

using System.Collections.Generic;

/*
 * ActivityLog handles all game centric events that happening during the game
 */
public class ActivityLog
{
    private static List<string> m_events = new List<string>(); 

    // AddActivity adds a new event to the List
    public static void AddActivity(string _event)
    {
        // Adds a new event to the log
        m_events.Add(_event);
        // Keeps the last events to conserve memory space
        if(m_events.Count > 15)
        {
            // Removes first index from list of events
            m_events.RemoveAt(0);
        }
    }

    // ClearLog clears an event from the List
    public static void ClearLog() {
        // Returns the last event logged
        m_events[m_events.Count - 1] = "";
    }

    // ReadRecentActivity returns the last event from the List
    public static string ReadRecentActivity()
    {
        // Checks if event log has any events added
        if(m_events.Count != 0)
        {
            // Returns the last event logged
            return m_events[m_events.Count - 1];
        }
        // No events logged, returns an empty string
        return "";
    }
}
