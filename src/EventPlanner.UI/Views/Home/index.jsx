import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';

const baseUrl = 'http://localhost:13692/';

class EventDetailLayout extends React.Component {
    render() {
        var center = { lat: 59.938043, lng: 30.337157 };
        var zoom = 9;
        return (
                <div className="panel panel-primary">
                    <div className="thumbnail ep-map">
                        <GoogleMap defaultCenter={center}
                                    defaultZoom={zoom}>
                            <div className="ep-marker">place A</div>
                        </GoogleMap>
                    </div>
                    <div className="panel-heading"><h4>Name of Selected Place</h4></div>
                    <table className="table table-striped">
                        <thead>
                            <tr>
                            <th></th>
                            <th colSpan="2">1. 1. 2013</th>
                            <th>2. 1. 2013</th>
                            </tr>
                            <tr>
                            <th></th>
                            <th>9:00</th>
                            <th>10:00</th>
                            <th>10:00</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>UserName</td>
                                <td><span className="glyphicon glyphicon-ok"></span></td>
                                <td><span className="glyphicon glyphicon-remove"></span></td>
                                <td><span className="glyphicon glyphicon-ok"></span></td>
                            </tr>
                            <tr>
                                <td><input type="text" className="form-control ep-width200" /></td>
                                <td><input type="checkbox" /></td>
                                <td><input type="checkbox" /></td>
                                <td><input type="checkbox" /></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
        );
    }
}

class EventDashboardLayout extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="panel panel-primary">
                    <div className="panel-heading">My events - Created</div>
                    <div className="panel-body">
                        <div className="row ep-event">
                            <h4 className="col-md-3">Event Name</h4>
                            <div className="col-md-9">
                                <div className="btn-group pull-right">
                                    <button className="btn btn-default navbar-btn nav-pills"><span className="glyphicon glyphicon-edit"></span>Edit event</button>
                                    <button className="btn btn-default navbar-btn nav-pills"><span className="glyphicon glyphicon-link"></span>Copy link</button>
                                    <button className="btn btn-default navbar-btn nav-pills"><span className="glyphicon glyphicon-copy"></span>Fill in</button>
                                </div>
                            </div>
                         </div>  
                    </div>
                </div>
            </div>
            );
    }
}

class EventEditLayout extends React.Component {
    render() {
        return (
            <div>
            <h1>test event edit layout render return</h1>
            hello lorem ipsum
            testing
            </div>
        );
    }
}

const eventEdit = document.getElementById('event-edit');
if (eventEdit) {
    ReactDOM.render(<EventEditLayout />, eventEdit);
}
const eventDetail = document.getElementById('event-detail');
if (eventDetail) {
    ReactDOM.render(<EventDetailLayout />, eventDetail);
}
const dashboard = document.getElementById('dashboard');
if (dashboard) {
    ReactDOM.render(<EventDashboardLayout />, dashboard);
}
