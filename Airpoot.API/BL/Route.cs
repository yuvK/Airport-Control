using System.Collections;

namespace Airpoot.API.BL
{
    public class Route : IEnumerable<Station>
    {
        Dictionary<Station, List<Station>> _route = new Dictionary<Station, List<Station>>();
        //public object ?GetDeparture { get; internal set; }
        public Station? FirstStation { get; set; }
        public bool isDeparture { get; set; }
        public void Add(Station station)
        {
            if (_route.ContainsKey(station))
                return;
                //throw new Exception("this station allready exists!");
            _route.Add(station, new List<Station>());
        }
        public void ConnectToStation(Station from, Station to)
        {
            if (!_route.ContainsKey(from) || !_route.ContainsKey(to))
                throw new Exception("this station is not exists!");
            _route[from].Add(to);
        }
        public List<Station> GetNext(Station station)
        {
            return _route[station];
        }
        public IEnumerator<Station> GetEnumerator()
        {
            return _route.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _route.Keys.GetEnumerator();
        }
    }
}