import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';

var eventData = {
    name : "Pavlova oslava narozek",
    desc : "Po roce se zase shledame, dame neco dobryho k jidlu a piti a poprejeme Pavlovi k jeho 25. narozkam.",
    people : ["john.smith77@gmail.com", "teri899@yahoo.com"],
    dates: [
        {
            value: "2. 3. 2016",
            hours: ["8:00","9:00", "10:00"]
        },
        {
            value: "2. 4. 2019",
            hours: ["10:00", "11:00"]
        }
    ],
    places: [{ lat: 59.938043, lng: 30.337157 }, { lat: 59.938, lng: 30.33 }]
}

class PeopleRows extends React.Component {
    render() {
        var rows = [];
        eventData.people.forEach(function (row)
        {
            rows.push(<h3>{row}</h3>);
        });

     
        return (
        <div className="form-group">
            <label htmlFor="personEmail0" className="col-sm-2 control-label">Person's email</label>
            <div className="col-sm-10">
            <input type="email" id="personEmail0" className="form-control" placeholder="john.smith@example.com" />
            </div>
            {rows}
        </div>
        );
    }
}

class EventEditLayout extends React.Component {
    render() {
        var center = { lat: 59.938043, lng: 30.337157 };
        var zoom = 9;

        let styles = {
                panel: {
                border: "solid #f1f1f1",
                borderWidth: "1 1px",
                borderRadius: "8px",

                padding: "15px"
                }
        }

        return (
            <div>

            <h2>Basic information</h2>
                <div style={styles.panel} >
                <form className="form-horizontal">
                <div className="form-group">
                    <label htmlFor="eventName" className="col-sm-2 control-label">Name</label>
                    <div className="col-sm-10">
                    <input type="text" id="eventName" className="form-control" placeholder="Event name" value={eventData.name}/>
                    </div>
                </div>
                <div className="form-group">
                    <label htmlFor="eventDesc" className="col-sm-2 control-label">Description</label>
                    <div className="col-sm-10">
                    <textarea id="eventDesc" className="form-control" rows="3">{eventData.desc}</textarea>
                    </div>
                </div>
                <div className="form-group">
                    <div className="col-sm-offset-2 col-sm-10">
                    <button type="submit" className="btn btn-default">Save event</button>
                    </div>
                </div>
                </form>
                </div>
            <h2>People</h2>
                <div style={styles.panel} >
                <form className="form-horizontal">
                    <button type="button" className="btn btn-default">Add people</button>
                    <PeopleRows/>
                </form>
                </div>
            <h2>Date and Time</h2>
                <div style={styles.panel} >
                <form className="form-horizontal">
                    <button type="button" className="btn btn-default">Add date</button>
                    <div className="form-group">
                        <label htmlFor="eventDate0" className="col-sm-2 control-label">Datetime</label>
                        <div className="col-sm-10">
                        <input type="datetime-local" id="eventDate0" className="form-control" />
                        </div>
                    </div>
                </form>
                </div>
            <h2>Place</h2>
                <div style={styles.panel} >
                <div className="thumbnail ep-map">
                    <GoogleMap defaultCenter={center}
                                apiKey={""}//get the key at https://developers.google.com/maps/documentation/javascript/get-api-key
                                defaultZoom={zoom}>
                        <div className="ep-marker">place A</div>
                    </GoogleMap>
                </div>
                <form className="form-horizontal">
                    <button type="button" className="btn btn-default">Add place</button>
                    <div className="form-group">
                        <label htmlFor="eventPlace0" className="col-sm-2 control-label">Place</label>
                        <div className="col-sm-10">
                        <input type="text" id="eventPlace0" className="form-control" />
                        </div>
                    </div>
                </form>
                </div>
            </div>
        );
    }
}

const eventEdit = document.getElementById('event-edit');
if (eventEdit) {
    ReactDOM.render(<EventEditLayout />, eventEdit);
}