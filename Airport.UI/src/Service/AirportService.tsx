import axios from "axios";

export function AirportService(){
    const url = "http://localhost:5143/api/airportHub";

    const getStatus = async () =>{
        console.log("Status updated");
        const res = await axios.get(url+"/get");
        return res.data;
    }


    return {getStatus}
}