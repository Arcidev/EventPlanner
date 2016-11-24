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

        return (
            <div>
            <h2>Basic information</h2>
                <div>
                <form class="form-horizontal">
                <div class="form-group">
                    <label for="eventName" class="col-sm-2 control-label">Name</label>
                    <div class="col-sm-10">
                    <input type="text" id="eventName" class="form-control" placeholder="Event name" />
                    </div>
                </div>
                <div class="form-group">
                    <label for="eventDesc" class="col-sm-2 control-label">Description</label>
                    <div class="col-sm-10">
                    <textarea id="eventDesc" class="form-control" rows="3"></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                    <button type="submit" class="btn btn-default">Save event</button>
                    </div>
                </div>
                </form>
                </div>
            <h2>People</h2>
                <div>
                <form class="form-horizontal">
                    <button type="button" class="btn btn-default">Add people</button>
                    <div class="form-group">
                        <label for="personEmail0" class="col-sm-2 control-label">Person's email</label>
                        <div class="col-sm-10">
                        <input type="email" id="personEmail0" class="form-control" placeholder="john.smith@example.com" />
                        </div>
                    </div>
                </form>
                </div>
            <h2>Date and Time</h2>
                <div>
                <form class="form-horizontal">
                    <button type="button" class="btn btn-default">Add date</button>
                    <div class="form-group">
                        <label for="eventDate0" class="col-sm-2 control-label">Datetime</label>
                        <div class="col-sm-10">
                        <input type="datetime-local" id="eventDate0" class="form-control" />
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
                <form class="form-horizontal">
                    <button type="button" class="btn btn-default">Add place</button>
                    <div class="form-group">
                        <label for="eventPlace0" class="col-sm-2 control-label">Place</label>
                        <div class="col-sm-10">
                        <input type="text" id="eventPlace0" class="form-control" />
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