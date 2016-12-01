import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';

class EventEditLayout extends React.Component {
    render() {
        var center = { lat: 59.938043, lng: 30.337157 };
        var zoom = 9;

        let styles = {
                panel: {
                border: "solid grey",
                borderWidth: "1 5px",
                borderRadius: "8px"
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
                    <input type="text" id="eventName" className="form-control" placeholder="Event name" />
                    </div>
                </div>
                <div className="form-group">
                    <label htmlFor="eventDesc" className="col-sm-2 control-label">Description</label>
                    <div className="col-sm-10">
                    <textarea id="eventDesc" className="form-control" rows="3"></textarea>
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
                <div>
                <form className="form-horizontal">
                    <button type="button" className="btn btn-default">Add people</button>
                    <div className="form-group">
                        <label htmlFor="personEmail0" className="col-sm-2 control-label">Person's email</label>
                        <div className="col-sm-10">
                        <input type="email" id="personEmail0" className="form-control" placeholder="john.smith@example.com" />
                        </div>
                    </div>
                </form>
                </div>
            <h2>Date and Time</h2>
                <div>
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
                <div>
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