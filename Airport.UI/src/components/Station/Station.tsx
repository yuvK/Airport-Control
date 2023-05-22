
import { IAirplaneDTO } from '../../Interfaces/IAirplaneDTO'
import { IStation } from '../../Interfaces/IStation'
import { Airplane } from '../Airplane/Airplane'
import './Station.css'


export function Station({ station, airplane }: { station: IStation, airplane: IAirplaneDTO }) {

    const { id } = station

    return <div className="stationDiv"
        style={{
            gridArea: `${station.y} / ${station.x} / span ${station.height} / span ${station.width}`,
        }}
    >
        <h4>{id}</h4>
        {/* <Airplane airplaneId={airplane.airplaneId} airplaneCode={airplane.airplaneCode} airplaneIsDeparture={airplane.airplaneIsDeparture} /> */}
        {/* <h6>{airplane.airplaneCode}</h6> */}
        {airplane.airplaneCode != "" && <Airplane airplaneId={airplane.airplaneId} airplaneCode={airplane.airplaneCode}
            airplaneIsDeparture={airplane.airplaneIsDeparture} />}
    </div>
}