import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';

class EventEditLayout extends React.Component {
    render() {
        return (
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
        );
    }
}

const eventEdit = document.getElementById('event-edit');
if (eventEdit) {
    ReactDOM.render(<EventEditLayout />, eventEdit);
}