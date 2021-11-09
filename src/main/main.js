import React from 'react';

import './main.sass';

import { HomePage } from './pages/homepage/homepage.js'
import Universe from './pages/universe/universe';

export default class Main extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            left: { left: '-100%' },
            imgWidth: 0,
            imgHeight: { height: '0px' },
        }
    }

    onLoad = () => {
        this.setState({ imgWidth: document.getElementById('universe-img').clientWidth })
        var height = document.getElementById('universe-img').clientHeight;
        console.log(height)
        this.setState({ imgHeight: { height: height + 'px' } })
    }

    updateOffset = () => {

        var containerWidth = document.getElementById('scroll-img').clientWidth;

        var offset = (this.state.imgWidth - containerWidth) / 2;
        this.setState({ left: { left: '-' + offset + 'px' } })

    };

    componentDidMount() {
        window.addEventListener('resize', this.updateOffset);
    }

    componentWillUnmount() {
        window.removeEventListener('resize', this.updateOffset);
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.imgWidth !== this.state.imgWidth) {
            this.updateOffset();
        }
    }

    render() {
        return (
            <>

                <HomePage isLoading={this.props.isLoading} />
                <div className='universe' id='universe'>
                    <Universe/>
                </div>
                <div className='bottom'>

                </div>

            </>

        );
    }

}