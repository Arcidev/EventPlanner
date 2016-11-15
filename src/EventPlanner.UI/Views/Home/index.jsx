import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';




const baseUrl = 'http://localhost:13692/';

var List = React.createClass({
    render: function() {
        var dict = this.props.data;
        return (<div>
            {  Object.getOwnPropertyNames(dict).map(function(item) {
              return <div>{dict[item]}</div>;
          })
            }
        </div>);
    }
});


class ChoiceTableRow extends React.Component {
    constructor()
    {
        super();
        this.state = {choices: [true,false,true,true], userMail: "ahoj", userId: -1};
    }

    render() {
        var choices = this.state.choices;

        return (
            <div className="row">
                <div className="btn btn-primary">
                    {this.state.userMail}
                </div>
                <div className="btn btn-default">
                    {choices.map(function(item) {
                        if(item)
                        {
                            return <span className="glyphicon glyphicon-ok-sign"></span>;
                        }
                        return <span className="glyphicon glyphicon-remove-sign"></span>;
                    })};
        </div>
    </div>
        );
    }
}

class EventDetailLayout extends React.Component {

    constructor()
    {
        super();
        this.state = {choices: []};
    }

    doStuff(e) {
        axios
            .get(`${baseUrl}/api/dashboard`)
            .then(response => {
                this.setState({
                    choices: response.data.choices
                });
            });
    }
    render() {
        var center = { lat: 59.938043, lng: 30.337157 };
        var zoom = 9;
        return (
                <div className="panel panel-primary">
                    <div className="thumbnail ep-map">
                        <GoogleMap
                            defaultCenter={center}
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
                                <td><input tyle="text" className="form-control" /></td>
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

const app = document.getElementById('event-detail');
ReactDOM.render(<EventDetailLayout />, app);
