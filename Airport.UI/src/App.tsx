import { createContext } from 'react'
import './App.css'
import { useSignalR } from './Hooks/useSignalR'
import { AirportService } from './Service/AirportService'
import { IStationDTO } from './Interfaces/IStationDTO'
import { IAirplaneDTO } from './Interfaces/IAirplaneDTO'
import {Map} from './components/Map/Map'
import { FlightsBoard } from './components/FlightsBoard/FlightsBoard'

export const airportContext = createContext<{
  stationDataQueue: IStationDTO[];
  flightsList:IAirplaneDTO[];
  
  getStatus: () => Promise<any>;

  
}>({} as any);
function App() {
  //const {startTimer, count} = useSignalR();
  //startTimer();

  const signal = useSignalR();
  const airportService = AirportService();



  return (<>
    <main>
      <h1>Airport Simulator</h1>
      <airportContext.Provider value={{ ...signal, ...airportService }}>
        <Map />
        <FlightsBoard />
      </airportContext.Provider>
    </main>
  </>)
}

export default App
